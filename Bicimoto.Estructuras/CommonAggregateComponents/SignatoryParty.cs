using System;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class SignatoryParty
    {
        public PartyIdentification PartyIdentification { get; set; }

        public PartyName PartyName { get; set; }

        public SignatoryParty()
        {
            PartyIdentification = new PartyIdentification();
            PartyName = new PartyName();
        }
    }
}