using Microsoft.Extensions.DependencyInjection;
using Money2.Domain.Consumptions;
using Money2.Domain.Incomes;
using Money2.Domain.Products;
using Money2.Domain.Users;

namespace Money2.Infrastructure.DI
{
    public static class InfrasctuctureLoader
    {
        public static void Load( IServiceCollection services )
        {
            services.AddScoped<IConsumptionRepository, ConsumptionRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
