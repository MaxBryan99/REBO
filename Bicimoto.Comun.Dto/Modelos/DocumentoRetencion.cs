#if !SILVERLIGHT

using Newtonsoft.Json;

#endif

using Bicimoto.Comun.Dto.Contratos;
using System.Collections.Generic;

namespace Bicimoto.Comun.Dto.Modelos
{
    public class DocumentoRetencion : DocumentoSunatBase, IDocumentoElectronico
    {
#if !SILVERLIGHT

        [JsonProperty(Order = 7, Required = Required.Always)]
#endif
        public string RegimenRetencion { get; set; }

#if !SILVERLIGHT

        [JsonProperty(Order = 8, Required = Required.Always)]
#endif
        public decimal TasaRetencion { get; set; }

#if !SILVERLIGHT

        [JsonProperty(Order = 9, Required = Required.Always)]
#endif
        public decimal ImporteTotalRetenido { get; set; }

#if !SILVERLIGHT

        [JsonProperty(Order = 10, Required = Required.Always)]
#endif
        public decimal ImporteTotalPagado { get; set; }

#if !SILVERLIGHT

        [JsonProperty(Order = 11, Required = Required.Always)]
#endif
        public List<ItemRetencion> DocumentosRelacionados { get; set; }
    }
}