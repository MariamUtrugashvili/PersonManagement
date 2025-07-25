using PersonManagement.Application.Caching;
using StackExchange.Redis;
using System.Text.Json;

namespace PersonManagement.Infrastructure.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _db = connectionMultiplexer.GetDatabase();
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _db.StringGetAsync(key);
            if (value.IsNullOrEmpty) return default;
            return JsonSerializer.Deserialize<T>(value!);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = JsonSerializer.Serialize(value);
            await _db.StringSetAsync(key, json, expiry);
        }

        public async Task RemoveAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
