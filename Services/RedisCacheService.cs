using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _db = connectionMultiplexer.GetDatabase();
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public Task InValidateKey(string keyPrefix)
        {
            EndPoint endPoint = _connectionMultiplexer.GetEndPoints().First();
            RedisKey[] keys = _connectionMultiplexer.GetServer(endPoint).Keys(pattern: $"{ keyPrefix }*").ToArray();
            _db.KeyDelete(keys);
            return Task.CompletedTask;
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            await _db.StringSetAsync(key, value);
        }

        public async Task SetCacheValueAsync(string key, string value, TimeSpan timeSpan)
        {
            await _db.StringSetAsync(key, value, timeSpan);
        }
    }
}
