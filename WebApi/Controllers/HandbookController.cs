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
            var types = _incomeService.GetIncomeTypes();

            return Ok( types );
        }

        [HttpGet( "places" )]
        public ActionResult GetPlaces()
        {
            var places = _consumptionService.GetConsumptionPlaces( UserId );

            return Ok( places );
        }

        [HttpGet( "productnames" )]
        public ActionResult GetProductNames()
        {
            var names = _productService.GetProductNames( UserId );

            return Ok( names );
        }

        [HttpGet( "producttypes" )]
        public ActionResult GetProductTypes()
        {
            var types = _productService.GetProductTypes();

            return Ok( types );
        }
    }
}
