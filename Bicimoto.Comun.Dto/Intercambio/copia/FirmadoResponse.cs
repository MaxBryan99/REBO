﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bicimoto.Comun.Dto.Intercambio
{
    public class FirmadoResponse : RespuestaComun
    {
        public string TramaXmlFirmado { get; set; }

        public string ResumenFirma { get; set; }

        public string ValorFirma { get; set; }
    }
}
