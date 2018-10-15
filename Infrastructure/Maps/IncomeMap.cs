using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money2.Domain.Incomes;
using Money2.Domain.Users;

namespace Money2.Infrastructure.Maps
{
    internal class IncomeMap : IEntityTypeConfiguration<Income>
    {
        public void Configure( EntityTypeBuilder<Income> builder )
        {
            builder.HasKey( p => p.Id );

            builder.Property( p => p.Name ).HasMaxLength( 255 ).IsRequired();
            builder.Property( p => p.Sum ).HasColumnType( "decimal(10,2)" );

            builder.HasOne<IncomeType>()
                .WithMany()
                .HasForeignKey( p => p.TypeId );

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey( p => p.UserId );
        }
    }
}
