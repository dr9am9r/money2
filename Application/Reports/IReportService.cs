using System;

namespace Money2.Application.Reports
{
    /// <summary>
    /// Сервис отчетов
    /// </summary>
    public interface IReportService
    {
        ReportsDto GetReports( Int32 userId );
    }
}
