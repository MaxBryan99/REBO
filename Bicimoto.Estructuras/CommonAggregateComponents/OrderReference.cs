using Bicimoto.Estructuras.CommonBasicComponents;
using System;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class OrderReference
    {
        public string Id { get; set; }

        public OrderTypeCode OrderTypeCode { get; set; }

        public OrderReference()
        {
            OrderTypeCode = new OrderTypeCode();
        }
    }
}