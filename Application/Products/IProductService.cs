using System;
using System.Collections.Generic;
using Money2.Domain.Products;

namespace Money2.Application.Products
{
    public interface IProductService
    {
        List<ProductDto> GetProducts( Int32 userId );

        List<String> GetProductNames( Int32 userId );

        List<ProductType> GetProductTypes();

        void DeleteProduct( Int32 userId, Int32 productId );

        void SaveProduct( ProductDto dto );
    }
}
