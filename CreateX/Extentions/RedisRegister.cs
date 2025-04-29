using StackExchange.Redis;

namespace CreateX.API.Extentions
{
    public static class RedisRegister
    {
        public static IServiceCollection RegisterRedis(this IServiceCollection services,IConfiguration config)
        {
            string redisConnection = config.GetConnectionString("Redis") ??
    "";


            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));

            return services;
        }
    }
}
