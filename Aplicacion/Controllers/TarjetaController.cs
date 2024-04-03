using Azure;
using Dominio.Dtos;
using Dominio.Enums;
using Dominio.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion.Controllers
{
    /// <summary>
    /// Controlador para las operaciones de la tarjeta.
    /// </summary>
    [ApiController]
    [Route("api/tarjeta")]
    public class TarjetaController : ControllerBase
    {
        private readonly ITarjetaRepository _tarjetaRepository;

        public TarjetaController(ITarjetaRepository tarjetaRepository)
        {
            _tarjetaRepository = tarjetaRepository;
        }

        /// <summary>
        /// Este endpoint obtiene las tarjetas de un cliente
        /// </summary>
        /// <param name="idCliente">Es el id del cliente</param>
        /// <returns>El response regresa un DTO con la infomación de las tarjetas del cliente.</returns>
        [HttpGet("{idCliente}")]
        public ActionResult<ResponseDataDto<List<TarjetaResponseDto>>> GetTarjetasPorCliente(int idCliente)
        {
            try
            {
                ResponseDataDto<List<TarjetaResponseDto>> response = _tarjetaRepository.ObtenerTarjetasPorCliente(idCliente);

                if (response.Mensajes.Where(m => m.Codigo == CodigoMensaje.NoEncontrado).DefaultIfEmpty().First() != null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }

                return StatusCode(StatusCodes.Status200OK, response);
                
            }
            catch (Exception)
            {
                ResponseDataDto<List<TarjetaResponseDto>> response = new();
                response.Estado = false;
                response.Mensajes.Add(new (CodigoMensaje.ErrorInterno, "Ha ocurrido un error al consultar la información de las tarjetas del cliente"));

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Este endpoint obtiene el detalle de una tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El response regresa un DTO con la infomación del detalle de la tarjeta.</returns>
        [HttpGet("{idTarjeta}/detalle")]
        public ActionResult<ResponseDataDto<TarjetaDetalleResponseDto>> GetTarjetaDetalle(int idTarjeta)
        {
            try
            {
                ResponseDataDto<TarjetaDetalleResponseDto> response = _tarjetaRepository.ObtenerTarjetaDetalle(idTarjeta);

                if (response.Mensajes.Where(m => m.Codigo == CodigoMensaje.NoEncontrado).DefaultIfEmpty().First() != null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception)
            {
                ResponseDataDto<TarjetaDetalleResponseDto> response = new();
                response.Estado = false;
                response.Mensajes.Add(new (CodigoMensaje.ErrorInterno, "Ha ocurrido un error al consultar la información de las tarjetas del cliente"));

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Este endpoint obtiene el historial de transacciones (compras y pagos) de una tarjeta tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El response regresa un DTO con la infomación del historial de la tarjeta tarjeta.</returns>
        [HttpGet("{idTarjeta}/historial")]
        public ActionResult<ResponseDataDto<List<TransaccionDto>>> ObtenerHistorialTarjeta(int idTarjeta)
        {
            try
            {
                ResponseDataDto<List<TransaccionDto>> response = _tarjetaRepository.ObtenerHistorialTarjeta(idTarjeta);

                if (response.Mensajes.Where(m => m.Codigo == CodigoMensaje.NoEncontrado).DefaultIfEmpty().First() != null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception)
            {
                ResponseDataDto<List<TransaccionDto>> response = new();
                response.Estado = false;
                response.Mensajes.Add(new(CodigoMensaje.ErrorInterno, "Ha ocurrido un error al consultar el historial de la tarjeta"));

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }
}
