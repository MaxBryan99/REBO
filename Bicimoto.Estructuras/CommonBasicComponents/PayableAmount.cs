using System;

namespace Bicimoto.Estructuras.CommonBasicComponents
{
    [Serializable]
    public class PayableAmount
    {
        public string CurrencyId { get; set; }

        public decimal Value { get; set; }

        public double MontoBase { get; set; }
    }
}