using System;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class ExchangeRate
    {
        public string SourceCurrencyCode { get; set; }

        public string TargetCurrencyCode { get; set; }

        public decimal CalculationRate { get; set; }

        public string Date { get; set; }
    }
}