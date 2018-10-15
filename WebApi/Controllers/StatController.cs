using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money2.Application.Reports;

namespace Money2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StatController : BaseController
    {
        private readonly IReportService _reportService;

        public StatController( IReportService reportService )
        {
            _reportService = reportService;
        }

        public ActionResult GetStats()
        {
            ReportsDto reports = _reportService.GetReports( UserId );

            return Ok( reports );
        }
    }
}
