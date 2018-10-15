using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money2.Application.Incomes;

namespace Money2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class IncomeController : BaseController
    {
        private readonly IIncomeService _incomeService;

        public IncomeController( IIncomeService incomeService )
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public ActionResult GetIncomes( [FromQuery]DateTime? startDate = null, [FromQuery]DateTime? endDate = null, [FromQuery]Int32 typeId = 0 )
        {
            List<IncomeDto> incomes = _incomeService.GetIncomes( UserId, startDate, endDate, typeId );

            return Ok( incomes );
        }

        [HttpPost]
        public ActionResult SaveIncome( [FromQuery]IncomeDto income )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            income.UserId = UserId;

            _incomeService.SaveIncome( income );

            return Ok( income.Id );
        }

        [HttpDelete]
        public ActionResult DeleteIncome( [FromQuery]Int32 id )
        {
            _incomeService.DeleteIncome( UserId, id );

            return Ok();
        }
    }
}
