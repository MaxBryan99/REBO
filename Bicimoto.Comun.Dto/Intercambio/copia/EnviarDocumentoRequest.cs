using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bicimoto.Comun.Dto.Intercambio
{
    public class EnviarDocumentoRequest : EnvioDocumentoComun
    {
        [JsonProperty(Required = Required.Always)]
        public string TramaXmlFirmado { get; set; }
    }
}
