using Newtonsoft.Json;

namespace Bicimoto.Comun.Dto.Intercambio
{
    public class ConsultaTicketRequest : EnvioDocumentoComun
    {
        [JsonProperty(Required = Required.Always)]
        public string NroTicket { get; set; }
    }
}