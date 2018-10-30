using System;
using System.Collections.Generic;
using System.Linq;
using Money2.Domain.Products;

namespace Money2.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService( IProductRepository productRepository )
        {
            _productRepository = productRepository;
        }

        public List<ProductDto> GetProducts( Int32 userId )
        {
            List<Product> products = _productRepository.GetProducts( userId );
            List<ProductType> productTypes = _productRepository.GetProductTypes();

            return products.ConvertAll( i => new ProductDto()
            {
                Id = i.Id,
                Name = i.Name,
                TypeId = i.TypeId,
                TypeName = productTypes.Find( it => it.Id == i.TypeId ).Name,
                UserId = i.UserId
            } );
        }

        public List<String> GetProductNames( Int32 userId )
        {
            return _productRepository.GetProducts( userId )
                    .Select( x => x.Name )
                    .Distinct()
                    .ToList();
        }

        public List<ProductType> GetProductTypes()
        {
            return _productRepository.GetProductTypes();
        }

        public void SaveProduct( ProductDto dto )
        {
            var product = new Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                TypeId = dto.TypeId,
                UserId = dto.UserId
            };

            _productRepository.Save( product );
        }

        public void DeleteProduct( int userId, int productId )
        {
            var income = _productRepository.Get( productId );
            if ( income != null && income.UserId == userId )
            {
                _productRepository.Delete( income );
            }
        }
    }
}
