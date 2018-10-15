using System;
using System.Collections.Generic;
using System.Linq;
using Money2.Domain.Products;

namespace Money2.Infrastructure
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository( Money2Context context ) : base( context ) { }

        public List<Product> GetProducts( Int32 userId )
        {
            return Query
                .Where( p => p.UserId == userId )
                .OrderBy( p => p.Name )
                .ToList();
        }

        public List<Product> GetProducts( String name, Int32 userId )
        {
            return Query
                .Where( p => p.UserId == userId && p.Name.Contains( name ) )
                .OrderBy( p => p.Name )
                .ToList();
        }

        public List<Product> GetProducts( Int32 typeId, Int32 userId )
        {
            return Query
                .Where( p => p.UserId == userId && p.TypeId == typeId )
                .OrderBy( p => p.Name )
                .ToList();
        }

        public List<ProductType> GetProductTypes()
        {
            return CommonQuery<ProductType>()
                .OrderBy( p => p.Name )
                .ToList();
        }

        public Dictionary<ProductType, Decimal> GetProductTypeStats( DateTime? monthYear, Int32 userId )
        {
            return null;

            //var result = new Dictionary<ProductType, Decimal>();

            //using ( var cmd = Connection.CreateCommand() )
            //{
            //    cmd.CommandText = @"SELECT pt.*, Sum(ci.Quantity * ci.Price) As Sum 
            //                    FROM ConsumptionItems ci 
            //                    JOIN Consumptions c ON ci.ConsumptionId = c.Id 
            //                    JOIN Products p ON ci.Name = p.Name AND p.UserId = @userId 
            //                    JOIN ProductTypes pt ON p.TypeId = pt.Id 
            //                    WHERE c.UserId = @userId AND (@monthYear IS NULL OR (MONTH(c.Date) = MONTH(@monthYear) AND YEAR(c.Date) = YEAR(@monthYear))) 
            //                    GROUP BY pt.Id, pt.Name";
            //    cmd.Parameters.Add( "@userId", MySqlDbType.Int32 ).Value = userId;
            //    cmd.Parameters.Add( "@monthYear", MySqlDbType.Date ).Value = monthYear;

            //    using ( var reader = cmd.ExecuteReader() )
            //    {
            //        while ( reader.Read() )
            //        {
            //            var type = FetchType( reader );

            //            result.Add( type, Convert.ToDecimal( reader[ "Sum" ] ) );
            //        }
            //    }
            //}

            //return result;
        }
    }
}