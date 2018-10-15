using System;

namespace Money2.Application.Consumptions
{
    public class ConsumptionDto
    {
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public String Place { get; set; }
        public Decimal Sum { get; set; }
        public Int32 UserId { get; set; }
    }
}
