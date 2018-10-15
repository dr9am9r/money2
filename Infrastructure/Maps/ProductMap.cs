using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money2.Domain.Products;
using Money2.Domain.Users;

namespace Money2.Infrastructure.Maps
{
    internal class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure( EntityTypeBuilder<Product> builder )
        {
            builder.HasKey( p => p.Id );

            builder.Property( p => p.Name ).HasMaxLength( 255 ).IsRequired();

            builder.HasOne<ProductType>()
                .WithMany()
                .HasForeignKey( p => p.TypeId );

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey( p => p.UserId );
        }
    }
}
