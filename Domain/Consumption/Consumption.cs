﻿using System;
using System.Collections.Generic;

namespace Money2.Domain.Consumptions
{
    public partial class Consumption : IEntity
    {
        public Int32 Id { get; set; }
        public DateTime Date { get; set; }
        public String Place { get; set; }
        public Decimal Sum { get; set; }
        public Int32 UserId { get; set; }
        public List<ConsumptionItem> ConsumptionItems { get; set; }
    }
}
