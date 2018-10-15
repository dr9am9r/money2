using System;

namespace Money2.Domain.Consumptions
{
    public partial class ConsumptionItem
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 Quantity { get; set; }

        public Decimal Price { get; set; }

        public Int32 ConsumptionId { get; set; }
    }
}
