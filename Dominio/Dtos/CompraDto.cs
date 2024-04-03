using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class CompraDto
    {
        public int IdCompra { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string Fecha { get; set; }
    }
}
