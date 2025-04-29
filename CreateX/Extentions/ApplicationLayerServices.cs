
using System.Globalization;
using API.Configuration;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Services.Implemnetation;
using Services.Services;

namespace Core.Extentions
{
    public static  class ApplicationLayerServices
    {
        public static IServiceCollection RegisterApplicatonLayerService(this IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContextPool<Context>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("CreateX"));

            });


           services.AddAutoMapper(typeof(Mappings).Assembly);

          services.AddLocalization(/*options => options.ResourcesPath = "Resources"*/);

           services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "ar" };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
                options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());

            });

            services.AddHealthChecks()
                 .AddSqlServer(config.GetConnectionString("CreateX"))
                     .AddRedis(config.GetConnectionString("Redis"));
                  


           services.AddHealthChecksUI(options => options.AddHealthCheckEndpoint("Health Check API", "/health")).AddInMemoryStorage();

            return services;
        }
    }
}
