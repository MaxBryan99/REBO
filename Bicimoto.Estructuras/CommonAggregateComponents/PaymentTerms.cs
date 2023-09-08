using System;
using Bicimoto.Estructuras.CommonBasicComponents;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class PaymentTerms
    {
        public string ID { get; set; }
        
        public string PaymentMeansID { get; set; }

    }
}
