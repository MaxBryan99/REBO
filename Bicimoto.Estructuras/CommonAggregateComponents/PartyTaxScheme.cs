using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class PartyTaxScheme
    {
        public string RegistrationName { get; set; }
        public CompanyID CompanyID { get; set; }
        public TaxScheme TaxScheme { get; set; }
        public RegistrationAddress RegistrationAddress { get; set; }
        public PartyTaxScheme()
        {
            CompanyID = new CompanyID();
            TaxScheme = new TaxScheme();
            RegistrationAddress = new RegistrationAddress();
        }

    }
}
