using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _db;

        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _db = connectionMultiplexer.GetDatabase();
        }

        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            await _db.StringSetAsync(key, value);
        }
    }
}
