
using API.Configuration;
using Infrastructure.DbContext;
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
            return services;
        }
    }
}
