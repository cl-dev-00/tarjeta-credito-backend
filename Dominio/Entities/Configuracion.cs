using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Configuracion
    {
        public string IdConfiguracion { get; set; }
        public string NombreConfiguracion { get; set; }
        public string DescripcionConfiguracion { get; set; }
        public string Estado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }

        public ICollection<ConfiguracionDetalle> ConfiguracionDetalles { get; set; }
    }
}
