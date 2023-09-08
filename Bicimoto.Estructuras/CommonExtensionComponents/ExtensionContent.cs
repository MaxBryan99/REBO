using Bicimoto.Estructuras.SunatAggregateComponents;
using System;

namespace Bicimoto.Estructuras.CommonExtensionComponents
{
    [Serializable]
    public class ExtensionContent
    {
        public AdditionalInformation AdditionalInformation { get; set; }

        public ExtensionContent()
        {
            AdditionalInformation = new AdditionalInformation();
        }
    }
}