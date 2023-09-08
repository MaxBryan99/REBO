using System;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class AccountingSupplierParty
    {
        public string CustomerAssignedAccountId { get; set; }

        public string AdditionalAccountId { get; set; }

        public string CodDomicilioFiscal { get; set; }

        public Party Party { get; set; }
    }
}