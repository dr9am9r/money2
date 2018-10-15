using System;
using System.Collections.Generic;

namespace Money2.Application.Reports.Products
{
    public class ProductStatDto
    {
        public String Type { get; set; }

        public List<Decimal> Sums { get; set; }

        public Decimal Total { get; set; }
    }
}
