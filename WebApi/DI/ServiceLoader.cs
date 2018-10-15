using Microsoft.Extensions.DependencyInjection;
using Money2.Application.Consumptions;
using Money2.Application.Incomes;
using Money2.Application.Products;
using Money2.Application.Reports;
using Money2.Application.Users;
using Money2.Domain.Consumptions;
using Money2.Domain.Incomes;
using Money2.Domain.Products;
using Money2.Domain.Users;
using Money2.Infrastructure;
using Money2.WebApi.Security;

namespace Money2.WebApi.DI
{
    internal static class ServiceLoader
    {
        public static void Load( IServiceCollection services )
        {
            services.AddScoped<ISecurityProvider, SecurityProvider>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IConsumptionRepository, ConsumptionRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IConsumptionService, ConsumptionService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
