using System;

namespace Money2.Domain.Incomes
{
    public partial class Income : IEntity
    {
        public Int32 Id { get; set; }

        public Decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public String Name { get; set; }

        public Int32 TypeId { get; set; }

        public Int32 UserId { get; set; }
    }
}
