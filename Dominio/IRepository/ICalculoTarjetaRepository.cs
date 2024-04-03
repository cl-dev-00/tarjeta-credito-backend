using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepository
{
    public interface ICalculoTarjetaRepository
    {
        /// <summary>
        /// Este método obtiene el calculo del interes Intereses Bonificable de una tarjeta
        /// </summary>
        /// <param name="saldoActual">Saldo de la tarjeta para poder realizar el calculo</param>
        /// <returns>El response regresa un DTO con la infomación del calculo realizado.</returns>
        public decimal ObtenerInteresesBonificable(decimal saldoActual);
        /// <summary>
        /// Este método obtiene el calculo del interes Cuota Minima a Pagar de una tarjeta
        /// </summary>
        /// <param name="saldoActual">Saldo de la tarjeta para poder realizar el calculo</param>
        /// <returns>El response regresa un DTO con la infomación del calculo realizado.</returns>
        public decimal ObtenerCuotaMinimaAPagar(decimal saldoActual);
        /// <summary>
        /// Este método obtiene el calculo del Monto Total Contado Con Intereses de una tarjeta
        /// </summary>
        /// <param name="saldoActual">Saldo de la tarjeta para poder realizar el calculo</param>
        /// <returns>El response regresa un DTO con la infomación del calculo realizado.</returns>
        public decimal ObtenerMontoTotalContadoConIntereses(decimal saldoActual);
    }
}
