﻿using Bicimoto.Estructuras.CommonBasicComponents;
using System;

namespace Bicimoto.Estructuras.CommonAggregateComponents
{
    [Serializable]
    public class DespatchLine
    {
        public int Id { get; set; }

        public InvoicedQuantity DeliveredQuantity { get; set; }

        /// <summary>
        /// cac:OrderLineReference/cbc:LineID
        /// </summary>
        public int OrderLineReferenceId { get; set; }

        public DespatchLineItem Item { get; set; }
    }
}