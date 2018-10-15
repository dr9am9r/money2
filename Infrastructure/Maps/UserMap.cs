using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Money2.Domain.Users;

namespace Money2.Infrastructure.Maps
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.HasKey( p => p.Id );

            builder.Property( p => p.Email ).HasMaxLength( 255 ).IsRequired();
            builder.Property( p => p.Name ).HasMaxLength( 255 ).IsRequired();
            builder.Property<String>( "Password" ).HasMaxLength( 255 ).IsRequired();

            builder.HasIndex( p => p.Email ).IsUnique();
        }
    }
}
