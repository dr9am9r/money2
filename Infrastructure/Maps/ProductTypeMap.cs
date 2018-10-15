using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money2.Domain.Products;

namespace Money2.Infrastructure.Maps
{
    internal class ProductTypeMap : IEntityTypeConfiguration<ProductType>
    {
        public void Configure( EntityTypeBuilder<ProductType> builder )
        {
            builder.HasKey( p => p.Id );

            builder.Property( p => p.Name ).HasMaxLength( 255 ).IsRequired();
            builder.Property( p => p.Code ).HasMaxLength( 63 );

            builder.HasIndex( p => p.Name ).IsUnique();
        }
    }
}
