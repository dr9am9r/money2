﻿using System;
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
        public ActionResult GetConsumptions( [FromQuery]DateTime? startDate = null, [FromQuery]DateTime? endDate = null, [FromQuery]Int32 typeId = 0 )
        {
            List<ConsumptionDto> consumptions = _consumptionService.GetConsumptions( UserId, startDate, endDate, typeId );

            return Ok( consumptions );
        }

        [HttpGet]
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
        public ActionResult SaveConsumption( [FromQuery]ConsumptionDto consumption )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            consumption.UserId = UserId;

            _consumptionService.SaveConsumption( consumption );

            return Ok( consumption.Id );
        }

        [HttpDelete]
        public ActionResult DeleteConsumption( [FromQuery]Int32 id )
        {
            _consumptionService.DeleteConsumption( UserId, id );

            return Ok();
        }
    }
}