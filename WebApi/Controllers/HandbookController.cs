using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money2.Application.Consumptions;
using Money2.Application.Incomes;
using Money2.Application.Products;

namespace Money2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HandbookController : BaseController
    {
        private readonly IConsumptionService _consumptionService;
        private readonly IIncomeService _incomeService;
        private readonly IProductService _productService;

        public HandbookController( IConsumptionService consumptionService, IIncomeService incomeService, IProductService productService )
        {
            _consumptionService = consumptionService;
            _incomeService = incomeService;
            _productService = productService;
        }

        [HttpGet( "incometypes" )]
        public ActionResult GetIncomeTypes()
        {
            try
            {
                var types = _incomeService.GetIncomeTypes();

                return Ok( types );
            }
            catch ( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        [ActionName( "places" )]
        public ActionResult GetPlaces()
        {
            try
            {
                var places = _consumptionService.GetConsumptionPlaces( UserId );

                return Ok( places );
            }
            catch ( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        [ActionName( "productnames" )]
        public ActionResult GetProductNames()
        {
            try
            {
                var names = _productService.GetProductNames( UserId );

                return Ok( names );
            }
            catch ( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }

        [ActionName( "producttypes" )]
        public ActionResult GetProductTypes()
        {
            try
            {
                var types = _productService.GetProductTypes();

                return Ok( types );
            }
            catch ( Exception ex )
            {
                return BadRequest( ex.Message );
            }
        }
    }
}
