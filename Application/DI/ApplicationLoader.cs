using Microsoft.Extensions.DependencyInjection;
using Money2.Application.Consumptions;
using Money2.Application.Incomes;
using Money2.Application.Products;
using Money2.Application.Reports;
using Money2.Application.Users;

namespace Money2.Application.DI
{
    public static class ApplicationLoader
    {
        public static void Load( IServiceCollection services )
        {
            services.AddScoped<IConsumptionService, ConsumptionService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
