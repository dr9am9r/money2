using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Money2.Infrastructure;
using Money2.WebApi.DI;
using Money2.WebApi.Security;

namespace Money2.WebApi
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc()
                .AddJsonOptions( opt => {
                    opt.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                } )
                .SetCompatibilityVersion( CompatibilityVersion.Version_2_1 );
            services.AddCors();

            services.AddDbContext<Money2Context>( options => options.UseMySql( Configuration.GetConnectionString( "Money2Database" ) ) );

            services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
                .AddJwtBearer( options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = JwtConfig.Issuer,
                        ValidAudience = JwtConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey( JwtConfig.SecretKey )
                    };
                } );

            ServiceLoader.Load( services );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors( options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );
            app.UseMvc();
        }
    }
}
