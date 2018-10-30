using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money2.Application.Consumptions;
using Money2.Application.Products;
using Money2.WebApi.Dtos;

namespace Money2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IConsumptionService _consumptionService;
        private readonly IProductService _productService;

        public ProductController( IConsumptionService consumptionService, IProductService productService )
        {
            _consumptionService = consumptionService;
            _productService = productService;
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            List<ProductDto> products = _productService.GetProducts( UserId );

            var result = new ProductsDto()
            {
                Products = products,
                Names = _consumptionService.GetConsumptionNames( UserId )
                    .Where( x => !products.Any( y => y.Name == x ) )
                    .ToList()
            };

            return Ok( result );
        }

        [HttpDelete( "{id}" )]
        public ActionResult DeleteProduct( Int32 id )
        {
            _productService.DeleteProduct( UserId, id );

            return Ok();
        }

        [HttpPost]
        public ActionResult SaveProduct( ProductDto product )
        {
            if ( !ModelState.IsValid )
            {
                return BadRequest( ModelState );
            }

            product.UserId = UserId;

            _productService.SaveProduct( product );

            return Ok( product.Id );
        }
    }
}
