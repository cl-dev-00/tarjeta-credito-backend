using Dominio.Dtos;
using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository
{
    public interface ITarjetaRepository
    {
        /// <summary>
        /// Este metódo obtiene las tarjetas de un cliente
        /// </summary>
        /// <param name="idCliente">Es el id del cliente</param>
        /// <returns>El metódo regresa un DTO con la infomación de las tarjetas del cliente.</returns>
        public ResponseDataDto<List<TarjetaResponseDto>> ObtenerTarjetasPorCliente(int idCliente);
        /// <summary>
        /// Este metódo obtiene el detalle de una tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El metódo regresa un DTO con la infomación del detalle de la tarjeta.</returns>
        public ResponseDataDto<TarjetaDetalleResponseDto> ObtenerTarjetaDetalle(int idTarjeta);
        /// <summary>
        /// Este metódo obtiene el historial de transacciones (compras y pagos) de una tarjeta tarjeta
        /// </summary>
        /// <param name="idTarjeta">Es el id de la tarjeta</param>
        /// <returns>El metódo regresa un DTO con la infomación del historial de la tarjeta tarjeta.</returns>
        public ResponseDataDto<List<TransaccionDto>> ObtenerHistorialTarjeta(int idTarjeta);
        /// <summary>
        /// Este metódo actualiza los diferentes saldos de una tarjeta por una compra
        /// </summary>
        /// <param name="tarjeta">Entidad de la tarjeta para actualizar sus saldos</param>
        /// <param name="montoCompra">Es el monto de la compra realizada</param>
        public void ActualizarSaldoTarjetaPorCompra(Tarjeta tarjeta, decimal montoCompra);
        /// <summary>
        /// Este metódo actualiza los diferentes saldos de una tarjeta por un pago
        /// </summary>
        /// <param name="tarjeta">Entidad de la tarjeta para actualizar sus saldos</param>
        /// <param name="montoPago">Es el monto del pago realizada</param>
        public void ActualizarSaldoTarjetaPorPago(Tarjeta tarjeta, decimal montoPago);
    }
}
