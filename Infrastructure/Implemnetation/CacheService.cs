
using System.Text.Json;

using Application.Services;

using StackExchange.Redis;

namespace Services.Implemnetation
{
    public class CacheService  : ICacheService
    {
        private readonly IConnectionMultiplexer redisConnection;
        private  IDatabase database;

        public CacheService(IConnectionMultiplexer redisConnection)
        {
            database = redisConnection.GetDatabase();
            this.redisConnection = redisConnection;
        }
        public async  Task<string> GetFromCache(string Key)
        {
            var cachedResponse = await database.StringGetAsync(Key);

            if (cachedResponse.IsNullOrEmpty) return null;

            return cachedResponse;
        }

        public async  Task SetInCache(string Key, object value,TimeSpan TTL)
        {
            if (value == null) return;

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var jsonSerializer = JsonSerializer.Serialize(value, options);

            await database.StringSetAsync(Key, jsonSerializer,TTL);
        }

        public async Task ClearAllAsync(string pattern)
        {
            var server = redisConnection.GetServer("redis-19318.crce177.me-south-1-1.ec2.redns.redis-cloud.com", 19318);
            var keys = server.Keys(pattern: pattern);
            foreach (var key in keys)
            {
                await database.KeyDeleteAsync(key);
            }
        }
    }
}
