using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Tarjeta
    {
        public int IdTarjeta { get; set; }
        public int IdCliente { get; set; }
        public string IdTipoTarjeta { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal SaldoDisponible { get; set; }
        public string Estado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }

        public virtual TipoTarjeta TipoTarjeta { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
