using AutoMapper;
using Dominio.Constants;
using Dominio.Dtos;
using Dominio.Entities;
using Dominio.Enums;
using Dominio.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repository
{
    /// <summary>
    /// Repositorio para operaciones relacionadas con pagos de tarjetas.
    /// </summary>
    public class PagoRepository : IPagoRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITarjetaRepository _tarjetaRepository;

        public PagoRepository(ApplicationDBContext dbContext, IMapper mapper, ITarjetaRepository tarjetaRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _tarjetaRepository = tarjetaRepository;
        }

        /// <summary>
        /// Este metódo crea y registra el pago de una tarjeta
        /// </summary>
        /// <param name="pagoRequest">Es un objecto data transfer object (DTO) utilizado para obtener la información del pago</param>
        /// <returns>El metódo regresa un DTO con la infomación de la compra ingresada.</returns>
        public ResponseDataDto<CrearPagoResponseDto> RegistrarPago(CrearPagoRequestDto pagoRequest)
        {
            ResponseDataDto<CrearPagoResponseDto> response = new();
            response.Estado = false;

            Cliente cliente = _dbContext.Cliente.Where(c => c.IdCliente == pagoRequest.IdCliente && c.Estado == Estados.Activo).DefaultIfEmpty().First();

            /*Validaciones*/

            if (cliente == null)
            {
                response.Mensajes.Add(new(CodigoMensaje.NoEncontrado, "El cliente no existe o no es un cliente habilitado"));
                return response;
            }

            Tarjeta tarjeta = _dbContext.Tarjeta.Where(t => t.IdCliente == pagoRequest.IdCliente && t.IdTarjeta == pagoRequest.IdTarjeta && t.Estado == Estados.Activo).DefaultIfEmpty().First();

            if (tarjeta == null)
            {
                response.Mensajes.Add(new(CodigoMensaje.NoEncontrado, "La tarjeta no existe o es una tarjeta inhabilitada"));
                return response;
            }

            if (pagoRequest.Monto > tarjeta.SaldoActual)
            {
                response.Mensajes.Add(new(CodigoMensaje.PeticionIncorrecta, "El monto del pago sobrepasa al saldo actual de la tarjeta"));
                return response;
            }

            /*Se agrega el nuevo pago en la base de datos */

            Pago pago = _mapper.Map<Pago>(pagoRequest);

            _dbContext.Pago.Add(pago);

            _dbContext.SaveChanges();

            /*se actualiza los saldos de la tarjeta*/
            _tarjetaRepository.ActualizarSaldoTarjetaPorPago(tarjeta, pagoRequest.Monto);

            CrearPagoResponseDto pagoResponse = _mapper.Map<CrearPagoResponseDto>(pago);

            response.Estado = true;
            response.Data = pagoResponse;
            response.Mensajes.Add(new(CodigoMensaje.Ok, "Se ha registrado con éxito el pago"));

            return response;
        }
    }
}
