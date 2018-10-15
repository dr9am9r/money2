using System.Linq;
using Microsoft.EntityFrameworkCore;
using Money2.Domain.Consumptions;
using Money2.Domain.Incomes;
using Money2.Domain.Products;
using Money2.Domain.Users;
using Money2.Infrastructure.Maps;

namespace Money2.Infrastructure
{
    public class Money2Context : DbContext
    {
        public Money2Context( DbContextOptions<Money2Context> options )
            : base( options ) { }

        public DbSet<Consumption> Consumptions { get; set; }

        public DbSet<ConsumptionItem> ConsumptionItems { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public DbSet<IncomeType> IncomeTypes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );

            builder.ApplyConfiguration( new ConsumptionMap() );
            builder.ApplyConfiguration( new ConsumptionItemMap() );
            builder.ApplyConfiguration( new IncomeMap() );
            builder.ApplyConfiguration( new IncomeTypeMap() );
            builder.ApplyConfiguration( new ProductMap() );
            builder.ApplyConfiguration( new ProductTypeMap() );
            builder.ApplyConfiguration( new UserMap() );
        }
    }
}
