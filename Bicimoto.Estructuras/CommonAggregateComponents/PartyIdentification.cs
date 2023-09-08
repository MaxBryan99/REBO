using Bicimoto.Estructuras.CommonBasicComponents;
using System;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class PartyIdentification
    {
        public PartyIdentificationId Id { get; set; }

        public PartyIdentification()
        {
            Id = new PartyIdentificationId();
        }
    }
}