using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class ResponseDataDto<T>
    {
        public Boolean Estado {  get; set; }
        public T? Data { get; set; }
        public List<MensajeResponseDto> Mensajes { get; set; } = new();
    }
}
