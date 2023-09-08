using Bicimoto.Estructuras.CommonBasicComponents;
using System;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class Price
    {
        public PayableAmount PriceAmount { get; set; }

        public Price()
        {
            PriceAmount = new PayableAmount();
        }
    }
}