
using System.Globalization;
using API.Configuration;
using Application.Common.Interfaces;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Services.Implemnetation;
using Application;
using API.Implemnetation;
using API.Extentions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using GoBeauti.Application.Common.Behaviours;
using MediatR;
using FluentValidation;
using Application.Areas.Account.Commands.Login;


namespace Core.Extentions
{
    public static  class ApplicationLayerServices
    {
        public static IServiceCollection RegisterApplicatonLayerService(this IServiceCollection services, IConfiguration config)
        {
            //Register services
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<Context>());

            //Add Database DI

            services.AddDbContextPool<Context>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("CreateX"));
            });

            //Add AutoMapper
            services.AddAutoMapper(x=>x.AddProfile(new Mappings()));


            // Register MediatR services and scan the Application assembly for handlers
           services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));


            //Register Localization ar-En 
            services.AddLocalization();

           services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { "en", "ar" };
                options.DefaultRequestCulture = new RequestCulture("en");
                options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
                options.SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                options.RequestCultureProviders = new[]
{
        new CustomRequestCultureProvider(context =>
        {
            var culture = context.Request.Headers["culture"].FirstOrDefault();
            if (string.IsNullOrEmpty(culture))
                culture = "en";

            return Task.FromResult(new ProviderCultureResult(culture, culture));
        })
    };

            });


            //add services Helth Checker

            services.AddHealthChecks()
                 .AddSqlServer(config.GetConnectionString("CreateX"))
                     .AddRedis(config.GetConnectionString("Redis"));
                  


           services.AddHealthChecksUI(options => options.AddHealthCheckEndpoint("Health Check API", "/health")).AddInMemoryStorage();



            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Period = TimeSpan.FromHours(3);
            });
            services.AddSingleton<IHealthCheckPublisher, EmailHealthCheckPublisher>();


            services.AddAntiforgery(options => {
                options.HeaderName = "X-XSRF-TOKEN";
            });

          

            services.Configure<ApiBehaviorOptions>(options =>
             {
                 options.SuppressModelStateInvalidFilter = true;
             });

            services.AddValidatorsFromAssembly(typeof(LoginCommandValidator).Assembly);

     

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }

        
    }
}
