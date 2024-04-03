using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class CrearPagoRequestDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int IdTarjeta { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int IdCliente { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(400)]
        public string NombreCliente { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Monto { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
    }
}
