using Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class MensajeResponseDto
    {
        public CodigoMensaje Codigo { get; set; }
        public string Descripcion { get; set; }
        
        public MensajeResponseDto(CodigoMensaje codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }

    }
}
