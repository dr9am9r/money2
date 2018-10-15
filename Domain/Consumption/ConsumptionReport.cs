using System;

namespace Money2.Domain.Consumptions
{
    public class ConsumptionReport
    {
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public Decimal Sum { get; set; }
    }
}