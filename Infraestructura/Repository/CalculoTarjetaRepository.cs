using Dominio.Constants;
using Dominio.Entities;
using Dominio.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repository
{
    /// <summary>
    /// Repositorio para operaciones relacionadas con cáculos de intereses de tarjetas.
    /// </summary>
    public class CalculoTarjetaRepository : ICalculoTarjetaRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private ConfiguracionDetalle porcentajeDeInteresConfiguracion { get; set; }
        private ConfiguracionDetalle porcentajeInteresSaldoMinimoConfiguracion { get; set; }

        public CalculoTarjetaRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;

            porcentajeDeInteresConfiguracion = _dbContext.ConfiguracionDetalle
                .Where(cd =>
                cd.IdConfiguracionDetalle == ConfiguracionesPorcentajesInteres.PorcentajeDeInteres &&
                cd.Estado == Estados.Activo).First();

            porcentajeInteresSaldoMinimoConfiguracion = _dbContext.ConfiguracionDetalle
                .Where(cd =>
                cd.IdConfiguracionDetalle == ConfiguracionesPorcentajesInteres.PorcentajeInteresSaldoMinimo &&
                cd.Estado == Estados.Activo).First();

        }

        /// <summary>
        /// Este método obtiene el calculo del interes Intereses Bonificable de una tarjeta
        /// </summary>
        /// <param name="saldoActual">Saldo de la tarjeta para poder realizar el calculo</param>
        /// <returns>El response regresa un DTO con la infomación del calculo realizado.</returns>
        public decimal ObtenerInteresesBonificable(decimal saldoActual)
        {
            decimal porcentajeDeInteresBonificable = decimal.Parse(porcentajeDeInteresConfiguracion.Valor1);
            
            decimal montoTotalAPagar = saldoActual * porcentajeDeInteresBonificable;

            return montoTotalAPagar;

        }

        /// <summary>
        /// Este método obtiene el calculo del interes Cuota Minima a Pagar de una tarjeta
        /// </summary>
        /// <param name="saldoActual">Saldo de la tarjeta para poder realizar el calculo</param>
        /// <returns>El response regresa un DTO con la infomación del calculo realizado.</returns>
        public decimal ObtenerCuotaMinimaAPagar(decimal saldoActual)
        {
            decimal porcentajeInteresSaldoMinimo = decimal.Parse(porcentajeInteresSaldoMinimoConfiguracion.Valor1);
            
            decimal cuotaMinimaAPagar = saldoActual * porcentajeInteresSaldoMinimo;

            return cuotaMinimaAPagar;
        }

        /// <summary>
        /// Este método obtiene el calculo del Monto Total Contado Con Intereses de una tarjeta
        /// </summary>
        /// <param name="saldoActual">Saldo de la tarjeta para poder realizar el calculo</param>
        /// <returns>El response regresa un DTO con la infomación del calculo realizado.</returns>
        public decimal ObtenerMontoTotalContadoConIntereses(decimal saldoActual)
        {
            decimal interesesBonificable = ObtenerInteresesBonificable(saldoActual);

            return saldoActual + interesesBonificable;

        }
    }
}
