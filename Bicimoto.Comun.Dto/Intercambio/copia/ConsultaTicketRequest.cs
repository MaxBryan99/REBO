using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bicimoto.Comun.Dto.Intercambio
{
    public class ConsultaTicketRequest : EnvioDocumentoComun
    {
        [JsonProperty(Required = Required.Always)]
        public string NroTicket { get; set; }
    }
}
