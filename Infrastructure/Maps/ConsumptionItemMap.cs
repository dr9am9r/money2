using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money2.Domain.Consumptions;

namespace Money2.Infrastructure.Maps
{
    internal class ConsumptionItemMap : IEntityTypeConfiguration<ConsumptionItem>
    {
        public void Configure( EntityTypeBuilder<ConsumptionItem> builder )
        {
            builder.HasKey( p => p.Id );

            builder.Property( p => p.Name ).HasMaxLength( 255 ).IsRequired();
            builder.Property( p => p.Price ).HasColumnType( "decimal(10,2)" );

            builder.HasOne<Consumption>()
                .WithMany( p => p.ConsumptionItems )
                .HasForeignKey( p => p.ConsumptionId );
        }
    }
}
