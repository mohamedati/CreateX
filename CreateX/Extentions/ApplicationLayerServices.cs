
using System.Globalization;
using API.Configuration;
using Application.Common.Interfaces;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Services.Implemnetation;
using Services.Services;
using AutoMapper;
using Application;


namespace Core.Extentions
{
    public static  class ApplicationLayerServices
    {
        public static IServiceCollection RegisterApplicatonLayerService(this IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<Context>());


            services.AddDbContextPool<Context>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("CreateX"));
            });

            services.AddAutoMapper(x=>x.AddProfile(new Mappings()));


            // Register MediatR services and scan the Application assembly for handlers
           services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

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
