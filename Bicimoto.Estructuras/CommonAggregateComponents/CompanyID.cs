using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class CompanyID
    {
        public string schemeID { get; set; }
        public string schemeName { get; set; }
        public string schemeAgencyName { get; set; }
        public string schemeURI { get; set; }

    }
}
