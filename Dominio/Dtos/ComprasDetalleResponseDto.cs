using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class ComprasDetalleResponseDto
    {
        public int MesActual {  get; set; }
        public int MesAnterior { get; set; }
        public decimal MontoTotalMesActual { get; set; }
        public decimal MontoTotalMesAnterior { get; set; }
        public List<CompraDto> compras { get; set; }
    }
}
