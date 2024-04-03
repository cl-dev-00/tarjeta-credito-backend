using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Enums
{
    public enum CodigoMensaje
    {
        Ok = 200,
        PeticionIncorrecta = 400,
        NoEncontrado = 404,
        ErrorInterno = 500
    }
}
