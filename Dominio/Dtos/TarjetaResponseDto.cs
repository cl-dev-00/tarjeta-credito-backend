using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class TarjetaResponseDto
    {
        public int IdTarjeta { get; set; }
        public string NombreTitular { get; set; }
        public decimal SaldoDisponible { get; set; }
        public string NumeroTarjeta { get; set;}
        public string TipoTarjeta { get; set; }
    }
}
