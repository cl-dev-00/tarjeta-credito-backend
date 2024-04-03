using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class TarjetaDetalleResponseDto
    {
        public int idTarjeta {  get; set; }
        public string NombreTitular {  get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal SaldoActual { get; set; }
        public decimal InteresBonificable { get; set; }
        public decimal CuotaMinima { get; set; }
        public decimal MontoTotalAPagar { get; set; }
        public decimal MontoTotalContadoConInteres { get; set; }
    }
}
