using Dominio.Dtos;
using Dominio.Enums;
using Dominio.IRepository;
using Infraestructura.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion.Controllers
{
    /// <summary>
    /// Controlador para las operaciones de pago.
    /// </summary>
    [ApiController]
    [Route("api/pago")]
    public class PagoController : Controller
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoController(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        /// <summary>
        /// Este endpoint crea y registra el pago de una tarjeta
        /// </summary>
        /// <param name="pago">Es un objecto data transfer object (DTO) utilizado para obtener la información del pago</param>
        /// <returns>El response regresa un DTO con la infomación de la compra ingresada.</returns>
        [HttpPost]
        public ActionResult<ResponseDataDto<CrearPagoResponseDto>> RegistrarCompra([FromBody] CrearPagoRequestDto pago)
        {
            try
            {
                ResponseDataDto<CrearPagoResponseDto> response = _pagoRepository.RegistrarPago(pago);

                if (response.Mensajes.Where(m => m.Codigo == CodigoMensaje.NoEncontrado).DefaultIfEmpty().First() != null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, response);
                }
                if (response.Mensajes.Where(m => m.Codigo == CodigoMensaje.PeticionIncorrecta).DefaultIfEmpty().First() != null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, response);
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception)
            {
                ResponseDataDto<CrearCompraResponseDto> response = new();
                response.Estado = false;
                response.Mensajes.Add(new(CodigoMensaje.ErrorInterno, "Ha ocurrido un error al intentar registrar el pago"));

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
