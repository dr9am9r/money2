using System;

namespace Money2.Application.Consumptions
{
    public class ConsumptionItemDto
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public Int32 Quantity { get; set; }

        public Decimal Price { get; set; }
    }
}
