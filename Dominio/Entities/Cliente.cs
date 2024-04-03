using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public string Estado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }

        public virtual ICollection<Tarjeta> Tarjetas { get; set; }
    }
}
