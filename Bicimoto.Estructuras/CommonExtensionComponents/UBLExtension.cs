using System;

namespace Bicimoto.Estructuras.CommonExtensionComponents
{
    [Serializable]
    public class UblExtension
    {
        public ExtensionContent ExtensionContent { get; set; }

        public UblExtension()
        {
            ExtensionContent = new ExtensionContent();
        }
    }
}