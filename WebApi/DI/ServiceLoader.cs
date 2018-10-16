using Microsoft.Extensions.DependencyInjection;
using Money2.Application.DI;
using Money2.Infrastructure.DI;
using Money2.WebApi.Security;

namespace Money2.WebApi.DI
{
    internal static class ServiceLoader
    {
        public static void Load( IServiceCollection services )
        {
            ApplicationLoader.Load( services );
            InfrasctuctureLoader.Load( services );

            services.AddScoped<ISecurityProvider, SecurityProvider>();
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}