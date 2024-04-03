using AutoMapper;
using Dominio.Constants;
using Dominio.Dtos;
using Dominio.Entities;
using Dominio.Enums;
using Dominio.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infraestructura.Repository
{

    public class TarjetaRepository : ITarjetaRepository
    {
        /// <summary>
        /// Repositorio para operaciones relacionadas con tarjetas.
        /// </summary>
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public TarjetaRepository(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Este metódo obtiene las tarjetas de un cliente
        /// </summary>
        /// <param name="idCliente">Es el id del cliente</param>
        /// <returns>El metódo regresa un DTO con la infomación de las tarjetas del cliente.</returns>
        public ResponseDataDto<List<TarjetaResponseDto>> ObtenerTarjetasPorCliente(int idCliente)
        {
            ResponseDataDto<List<TarjetaResponseDto>> response = new ();
            response.Estado = false;

            Cliente cliente = _dbContext.Cliente.Where(c => c.IdCliente == idCliente && c.Estado == Estados.Activo).DefaultIfEmpty().First();

            if(cliente == null)
            {
                response.Mensajes.Add(new (CodigoMensaje.NoEncontrado, "El cliente no existe"));
                return response;
            }

            var listaTarjetas = _dbContext.Tarjeta.Where(t => t.IdCliente == idCliente && t.Estado == Estados.Activo)
                .Include(t => t.Cliente)
                .Include(t => t.TipoTarjeta)
                .DefaultIfEmpty().ToList();

            if (listaTarjetas == null || listaTarjetas?.Count == 0)
            {
                response.Mensajes.Add(new (CodigoMensaje.NoEncontrado, "El cliente no existe o no es un cliente habilitado"));
                return response;
            }

            List<TarjetaResponseDto> listaTarjetaResponse = _mapper.Map<List<TarjetaResponseDto>>(listaTarjetas);


            response.Estado = true;
            response.Data = listaTarjetaResponse;
            response.Mensajes.Add(new (CodigoMensaje.Ok, "Se ha obtenido la información de las tarjetas del cliente de forma exitosa"));


            return response;
        }

        /// <summary>
        /// Este metódo obtiene el detalle de una tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El metódo regresa un DTO con la infomación del detalle de la tarjeta.</returns>
        public ResponseDataDto<TarjetaDetalleResponseDto> ObtenerTarjetaDetalle(int idTarjeta)
        {
            ResponseDataDto<TarjetaDetalleResponseDto> response = new ();
            response.Estado = false;

            Tarjeta tarjeta = _dbContext.Tarjeta.Where(t => t.IdTarjeta == idTarjeta && t.Estado == Estados.Activo)
                .Include(t => t.Cliente)
                .Include(t => t.TipoTarjeta)
                .DefaultIfEmpty().First();

            if (tarjeta == null)
            {
                response.Mensajes.Add(new (CodigoMensaje.NoEncontrado, "La tarjeta no existe o es una tarjeta inhabilitada"));
                return response;
            }

            if (tarjeta.Cliente == null || tarjeta.Cliente?.Estado == Estados.Inactivo)
            {
                response.Mensajes.Add(new (CodigoMensaje.NoEncontrado, "El cliente no existe o no es un cliente habilitado"));
                return response;
            }

            TarjetaDetalleResponseDto tarjetaDetalleResponseDto = _mapper.Map<TarjetaDetalleResponseDto>(tarjeta);

            response.Estado = true;
            response.Data = tarjetaDetalleResponseDto;
            response.Mensajes.Add(new (CodigoMensaje.Ok, "Se ha obtenido la información de la tarjeta de forma exitosa"));

            return response;
        }

        /// <summary>
        /// Este metódo obtiene el historial de transacciones (compras y pagos) de una tarjeta tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El metódo regresa un DTO con la infomación del historial de la tarjeta tarjeta.</returns>
        public ResponseDataDto<List<TransaccionDto>> ObtenerHistorialTarjeta(int idTarjeta)
        {
            ResponseDataDto<List<TransaccionDto>> response = new();
            response.Estado = false;

            /*Validaciones*/

            Tarjeta tarjeta = _dbContext.Tarjeta.Where(t => t.IdTarjeta == idTarjeta && t.Estado == Estados.Activo)
                .Include(t => t.Cliente)
                .DefaultIfEmpty()
                .First();

            if (tarjeta == null)
            {
                response.Mensajes.Add(new(CodigoMensaje.NoEncontrado, "La tarjeta no existe o es una tarjeta inhabilitada"));
                return response;
            }

            /*Parametros para ejecutar el Stored Procedure*/

            var parameters = new List<SqlParameter>();

            var parameterIdTarjeta = new SqlParameter
            {
                ParameterName = "idTarjeta",
                SqlDbType = System.Data.SqlDbType.Int,
                Value = idTarjeta,
            };

            parameters.Add(parameterIdTarjeta);


            string sql = "EXEC ObtenerHistorialDeLaTarjeta @idTarjeta";

            /*Ejecuto el SP para obtener el mes actual y anterior además de los monto del mes actual y anterior*/

            var resultado = string.Join("", _dbContext.Database.SqlQueryRaw<string>(sql, parameters.ToArray())
                .ToList());

            List<TransaccionDto> transacciones = JsonSerializer.Deserialize<List<TransaccionDto>>(resultado);

            response.Estado = true;
            response.Data = transacciones;
            response.Mensajes.Add(new(CodigoMensaje.Ok, "Se ha obtenido la información del historial de la tarjeta"));

            return response;
        }

        /// <summary>
        /// Este metódo actualiza los diferentes saldos de una tarjeta por una compra
        /// </summary>
        /// <param name="tarjeta">Entidad de la tarjeta para actualizar sus saldos</param>
        /// <param name="montoCompra">Es el monto de la compra realizada</param>
        public void ActualizarSaldoTarjetaPorCompra(Tarjeta tarjeta, decimal montoCompra)
        {
            tarjeta.SaldoActual += montoCompra;
            
            tarjeta.SaldoDisponible -= montoCompra;

            _dbContext.Tarjeta.Update(tarjeta);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Este metódo actualiza los diferentes saldos de una tarjeta por un pago
        /// </summary>
        /// <param name="tarjeta">Entidad de la tarjeta para actualizar sus saldos</param>
        /// <param name="montoPago">Es el monto del pago realizada</param>
        public void ActualizarSaldoTarjetaPorPago(Tarjeta tarjeta, decimal montoPago)
        {
            tarjeta.SaldoActual -= montoPago;

            tarjeta.SaldoDisponible += montoPago;

            _dbContext.Tarjeta.Update(tarjeta);

            _dbContext.SaveChanges();
        }
        
    }
}
