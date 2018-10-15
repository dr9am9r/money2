using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money2.Domain.Consumptions;
using Money2.Domain.Users;

namespace Money2.Infrastructure.Maps
{
    internal class ConsumptionMap : IEntityTypeConfiguration<Consumption>
    {
        public void Configure( EntityTypeBuilder<Consumption> builder )
        {
            builder.HasKey( p => p.Id );

            builder.Property( p => p.Place ).HasMaxLength( 255 ).IsRequired();
            builder.Property( p => p.Sum ).HasColumnType( "decimal(10,2)" );

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey( p => p.UserId );
        }
    }
}
