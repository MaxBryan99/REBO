using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bicimoto.Comun.Dto.Intercambio
{
    public abstract class RespuestaComunConArchivo : RespuestaComun
    {
        public string NombreArchivo { get; set; }
    }
}
