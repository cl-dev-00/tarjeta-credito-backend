using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public int IdTarjeta { get; set; }
        public int IdCliente { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }

        public virtual Tarjeta Tarjeta { get; set; }
    }
}
