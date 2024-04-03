using Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository
{
    public interface ICompraRepository
    {
        /// <summary>
        /// Este método obtiene el historial de compras de una tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El método regresa un DTO con la infomación del historial de compras realizadas con la tarjeta.</returns>
        public ResponseDataDto<ComprasDetalleResponseDto> ObtenerDetalleDeLasCompras(int idTarjeta);
        /// <summary>
        /// Este método crea y registra una compra hecha con una tarjeta
        /// </summary>
        /// <param name="compraRequest">Es un objecto data transfer object (DTO) utilizado para obtener la información de la compra</param>
        /// <returns>El método regresa un DTO con la infomación de la compra ingresada.</returns>
        public ResponseDataDto<CrearCompraResponseDto> RegistrarCompra(CrearCompraRequestDto compraRequest);
    }
}
