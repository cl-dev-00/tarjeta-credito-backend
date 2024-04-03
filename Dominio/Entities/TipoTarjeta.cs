using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class TipoTarjeta
    {
        public string IdTipoTarjeta { get; set; }
        public string NombreTipoTarjeta { get; set; }
        public string DescripcionTipoTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public string Estado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }

        public virtual ICollection<Tarjeta> Tarjetas { get; set; }
    }
}
