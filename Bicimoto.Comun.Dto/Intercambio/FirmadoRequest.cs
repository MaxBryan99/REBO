﻿using Newtonsoft.Json;

namespace Bicimoto.Comun.Dto.Intercambio
{
    public class FirmadoRequest
    {
        [JsonProperty(Required = Required.Always)]
        public string CertificadoDigital { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string PasswordCertificado { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string TramaXmlSinFirma { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool UnSoloNodoExtension { get; set; }
    }
}