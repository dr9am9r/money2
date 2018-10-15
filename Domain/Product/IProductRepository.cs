using System;
using System.Collections.Generic;

namespace Money2.Domain.Products
{
    /// <summary>
    /// Репозиторий Товаров
    /// </summary>
    public interface IProductRepository
    {
        void Delete( Product product );

        Product Get( Int32 id );

        List<Product> GetProducts( Int32 userId );

        List<Product> GetProducts( String name, Int32 userId );

        List<Product> GetProducts( Int32 typeId, Int32 userId );

        List<ProductType> GetProductTypes();

        Dictionary<ProductType, Decimal> GetProductTypeStats( DateTime? monthYear, Int32 userId );

        void Save( Product product );
    }
}
