using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money2.Application.Consumptions;

namespace Money2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ConsumptionController : BaseController
    {
        private readonly IConsumptionService _consumptionService;

        public ConsumptionController( IConsumptionService consumptionService )
        {
            _consumptionService = consumptionService;
        }

        [HttpGet]
        public ActionResult GetConsumptions( DateTime? startDate = null, DateTime? endDate = null, Int32 typeId = 0 )
        {
            List<ConsumptionDto> consumptions = _consumptionService.GetConsumptions( UserId, startDate, endDate, typeId );

            return Ok( consumptions );
        }

        [HttpGet( "{id}" )]
        public ActionResult GetConsumption( Int32 id )
        {
            var consumption = _consumptionService.GetConsumption( UserId, id );
            if ( consumption == null )
            {
                return NotFound();
            }

            return Ok( consumption );
        }

        [HttpPost]
        public ActionResult SaveConsumption( [FromBody]ConsumptionDto consumption )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            consumption.UserId = UserId;

            _consumptionService.SaveConsumption( consumption );

            return Ok( consumption.Id );
        }

        [HttpDelete( "{id}" )]
        public ActionResult DeleteConsumption( Int32 id )
        {
            _consumptionService.DeleteConsumption( UserId, id );

            return Ok();
        }
    }
}