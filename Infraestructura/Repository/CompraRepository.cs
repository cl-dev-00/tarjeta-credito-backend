using AutoMapper;
using Dominio.Constants;
using Dominio.Dtos;
using Dominio.Entities;
using Dominio.Enums;
using Dominio.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repository
{
    /// <summary>
    /// Repositorio para operaciones relacionadas con compras de tarjetas.
    /// </summary>
    public class CompraRepository : ICompraRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITarjetaRepository _tarjetaRepository;

        public CompraRepository(ApplicationDBContext dbContext, IMapper mapper, ITarjetaRepository tarjetaRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tarjetaRepository = tarjetaRepository;
        }

        /// <summary>
        /// Este método obtiene el historial de compras de una tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El método regresa un DTO con la infomación del historial de compras realizadas con la tarjeta.</returns>
        public ResponseDataDto<ComprasDetalleResponseDto> ObtenerDetalleDeLasCompras(int idTarjeta)
        {
            ResponseDataDto<ComprasDetalleResponseDto> response = new();
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
            var parameterMesActual = new SqlParameter
            {
                ParameterName = "mesActual",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            var parameterMesAnterior = new SqlParameter
            {
                ParameterName = "mesAnterior",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            var parameterMontoTotalMesActual = new SqlParameter
            {
                ParameterName = "montoMesActual",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Output,
            };
            var parameterMontoTotalMesAnterior = new SqlParameter
            {
                ParameterName = "montoMesAnterior",
                SqlDbType = System.Data.SqlDbType.Decimal,
                Direction = System.Data.ParameterDirection.Output,
            };

            parameters.Add(parameterIdTarjeta);
            parameters.Add(parameterMesActual);
            parameters.Add(parameterMesAnterior);
            parameters.Add(parameterMontoTotalMesActual);
            parameters.Add(parameterMontoTotalMesAnterior);


            string sql = "EXEC MontoTotalDeLasCompras @idTarjeta, @mesActual OUTPUT, @mesAnterior OUTPUT, @montoMesActual OUTPUT, @montoMesAnterior OUTPUT";

            /*Ejecuto el SP para obtener el mes actual y anterior además de los monto del mes actual y anterior*/

            _dbContext.Database.SqlQueryRaw<string>(sql, parameters.ToArray()).ToList();
    
            ComprasDetalleResponseDto comprasDetalleResponseDto = new();
            comprasDetalleResponseDto.MesAnterior = Convert.ToInt32(parameterMesAnterior.Value);
            comprasDetalleResponseDto.MesActual = Convert.ToInt32(parameterMesActual.Value);
            comprasDetalleResponseDto.MontoTotalMesAnterior = Convert.ToInt32(parameterMontoTotalMesAnterior.Value);
            comprasDetalleResponseDto.MontoTotalMesActual = Convert.ToInt32(parameterMontoTotalMesActual.Value);


            List<Compra> listadoCompras = _dbContext.Compra.Where(c => c.Estado == Estados.Activo)
                .OrderByDescending(c => c.Fecha)
                .DefaultIfEmpty()
                .ToList();

            comprasDetalleResponseDto.compras = _mapper.Map<List<CompraDto>>(listadoCompras);

            response.Estado = true;
            response.Data = comprasDetalleResponseDto;
            response.Mensajes.Add(new(CodigoMensaje.Ok, "Se ha obtenido la información de las compras con exito"));

            return response;
        }

        /// <summary>
        /// Este método crea y registra una compra hecha con una tarjeta
        /// </summary>
        /// <param name="compraRequest">Es un objecto data transfer object (DTO) utilizado para obtener la información de la compra</param>
        /// <returns>El método regresa un DTO con la infomación de la compra ingresada.</returns>
        public ResponseDataDto<CrearCompraResponseDto> RegistrarCompra(CrearCompraRequestDto compraRequest)
        {
            ResponseDataDto<CrearCompraResponseDto> response = new();
            response.Estado = false;
            
            /*Validaciones*/

            Cliente cliente = _dbContext.Cliente.Where(c => c.IdCliente == compraRequest.IdCliente && c.Estado == Estados.Activo).DefaultIfEmpty().First();

            if(cliente == null)
            {
                response.Mensajes.Add(new (CodigoMensaje.NoEncontrado, "El cliente no existe o no es un cliente habilitado"));
                return response;
            }

            Tarjeta tarjeta = _dbContext.Tarjeta.Where(t => t.IdCliente == compraRequest.IdCliente && t.IdTarjeta == compraRequest.IdTarjeta && t.Estado == Estados.Activo).DefaultIfEmpty().First();

            if (tarjeta == null)
            {
                response.Mensajes.Add(new (CodigoMensaje.NoEncontrado, "La tarjeta no existe o es una tarjeta inhabilitada"));
                return response;
            }

            if (compraRequest.Monto > tarjeta.SaldoDisponible)
            {
                response.Mensajes.Add(new(CodigoMensaje.PeticionIncorrecta, "La tarjeta no dispone del saldo necesario para realizar la compra"));
                return response;
            }

            /*Se agrega la nueva compra en la base de datos */

            Compra compra = _mapper.Map<Compra>(compraRequest);

            _dbContext.Compra.Add(compra);

            _dbContext.SaveChanges();

            /*se actualiza los saldos de la tarjeta*/

            _tarjetaRepository.ActualizarSaldoTarjetaPorCompra(tarjeta, compraRequest.Monto);

            CrearCompraResponseDto compraResponse = _mapper.Map<CrearCompraResponseDto>(compra);

            response.Estado = true;
            response.Data = compraResponse;
            response.Mensajes.Add(new (CodigoMensaje.Ok, "Se ha registrado con éxito la compra"));

            return response;
        }
    }
}
