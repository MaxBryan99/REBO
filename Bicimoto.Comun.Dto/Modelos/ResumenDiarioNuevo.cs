﻿using System.Collections.Generic;

#if !SILVERLIGHT

using Newtonsoft.Json;

#endif

namespace Bicimoto.Comun.Dto.Modelos
{
    public class ResumenDiarioNuevo : DocumentoResumen
    {
#if !SILVERLIGHT

        [JsonProperty(Required = Required.Always)]
#endif
        public List<GrupoResumenNuevo> Resumenes { get; set; }
    }
}