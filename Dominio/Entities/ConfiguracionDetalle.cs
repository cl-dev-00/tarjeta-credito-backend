using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class ConfiguracionDetalle
    {
        public string IdConfiguracionDetalle { get; set; }
        public string IdConfiguracion { get; set; }
        public string NombreConfiguracionDetalle { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }
        public string Estado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaHoraCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime? FechaHoraModificacion { get; set; }

        public virtual Configuracion Configuracion { get; set; }
    }
}
