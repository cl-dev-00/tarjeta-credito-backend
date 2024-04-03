using Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository
{
    public interface IPagoRepository
    {
        /// <summary>
        /// Este metódo crea y registra el pago de una tarjeta
        /// </summary>
        /// <param name="pagoRequest">Es un objecto data transfer object (DTO) utilizado para obtener la información del pago</param>
        /// <returns>El metódo regresa un DTO con la infomación de la compra ingresada.</returns>
        public ResponseDataDto<CrearPagoResponseDto> RegistrarPago(CrearPagoRequestDto pagoRequest);
    }
}
