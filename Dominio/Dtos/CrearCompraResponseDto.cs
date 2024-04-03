using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class CrearCompraResponseDto
    {
        public int IdCompra { get; set; }
        public int IdTarjeta { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Fecha {  get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
    }
}
