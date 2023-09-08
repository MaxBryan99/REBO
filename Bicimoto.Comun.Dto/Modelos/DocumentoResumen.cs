#if !SILVERLIGHT

using Newtonsoft.Json;

#endif

using Bicimoto.Comun.Dto.Contratos;

namespace Bicimoto.Comun.Dto.Modelos
{
    public abstract class DocumentoResumen : IDocumentoElectronico
    {
#if !SILVERLIGHT

        [JsonProperty(Required = Required.Always)]
#endif
        public string IdDocumento { get; set; }

#if !SILVERLIGHT

        [JsonProperty(Required = Required.Always)]
#endif
        public string FechaEmision { get; set; }

#if !SILVERLIGHT

        [JsonProperty(Required = Required.Always)]
#endif
        public string FechaReferencia { get; set; }

#if !SILVERLIGHT

        [JsonProperty(Required = Required.Always)]
#endif
        public Contribuyente Emisor { get; set; }
    }
}