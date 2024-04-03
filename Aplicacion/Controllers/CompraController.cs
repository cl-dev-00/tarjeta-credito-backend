using Dominio.Constants;
using Dominio.Dtos;
using Dominio.Entities;
using Dominio.Enums;
using Dominio.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Aplicacion.Controllers
{
    /// <summary>
    /// Controlador para las operaciones de compra.
    /// </summary>
    [ApiController]
    [Route("api/compra")]
    public class CompraController: ControllerBase
    {
        private ICompraRepository _compraRepository;

        public CompraController(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        /// <summary>
        /// Este endpoint obtiene el historial de compras de una tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El response regresa un DTO con la infomación del historial de compras realizadas con la tarjeta.</returns>
        [HttpGet("{idTarjeta}")]
        public ActionResult<ResponseDataDto<ComprasDetalleResponseDto>> ObtenerDetalleDeLasCompras(int idTarjeta) {
            try
            {
                ResponseDataDto<ComprasDetalleResponseDto> response = _compraRepository.ObtenerDetalleDeLasCompras(idTarjeta);

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
                ResponseDataDto<ComprasDetalleResponseDto> response = new();
                response.Estado = false;
                response.Mensajes.Add(new(CodigoMensaje.ErrorInterno, "Ha ocurrido un error al intentar obtener la informacións de las compras"));

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Este endpoint crea y registra una compra hecha con una tarjeta
        /// </summary>
        /// <param name="compra">Es un objecto data transfer object (DTO) utilizado para obtener la información de la compra</param>
        /// <returns>El response regresa un DTO con la infomación de la compra ingresada.</returns>
        [HttpPost]
        public ActionResult<ResponseDataDto<CrearCompraResponseDto>> RegistrarCompra([FromBody] CrearCompraRequestDto compra)
        {
            try
            {
                ResponseDataDto<CrearCompraResponseDto> response = _compraRepository.RegistrarCompra(compra);

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
                response.Mensajes.Add(new(CodigoMensaje.ErrorInterno, "Ha ocurrido un error al intentar registrar la compra"));

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
