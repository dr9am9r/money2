using System;
using System.Collections.Generic;
using Money2.Application.Products;

namespace Money2.WebApi.Dtos
{
    public class ProductsDto
    {
        public List<ProductDto> Products { get; set; }

        public List<String> Names { get; set; }
    }
}
