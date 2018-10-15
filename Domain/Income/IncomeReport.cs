using System;

namespace Money2.Domain.Incomes
{
    public class IncomeReport
    {
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public Decimal Sum { get; set; }
    }
}