using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money2.Domain.Incomes;

namespace Money2.Infrastructure.Maps
{
    internal class IncomeTypeMap : IEntityTypeConfiguration<IncomeType>
    {
        public void Configure( EntityTypeBuilder<IncomeType> builder )
        {
            builder.HasKey( p => p.Id );

            builder.Property( p => p.Name ).HasMaxLength( 255 ).IsRequired();

            builder.HasIndex( p => p.Name ).IsUnique();
        }
    }
}
