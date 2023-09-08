using Bicimoto.Estructuras.CommonBasicComponents;
using System;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class AlternativeConditionPrice
    {
        public PayableAmount PriceAmount { get; set; }

        public string PriceTypeCode { get; set; }

        public AlternativeConditionPrice()
        {
            PriceAmount = new PayableAmount();
        }
    }
}