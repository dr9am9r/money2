using System;
using System.Collections.Generic;
using Money2.Application.Reports.Charts;
using Money2.Application.Reports.Incomes;
using Money2.Application.Reports.Products;

namespace Money2.Application.Reports
{
    public class ReportsDto
    {
        /// <summary>
        /// Доходы
        /// </summary>
        public List<IncomeStatDto> IncomeStats { get; set; }

        /// <summary>
        /// Расходы
        /// </summary>
        public List<ProductStatDto> ProductStats { get; set; }

        public List<Month> Months { get; set; }

        public Int32 TotalMonths { get; set; }

        public ChartDto[] Charts { get; set; }
    }
}
