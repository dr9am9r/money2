using System;

namespace Money2.Application.Reports.Charts
{
    public class ChartDto
    {
        public String[] Labels { get; set; }

        public SeriesDto[] Series { get; set; }
    }
}
