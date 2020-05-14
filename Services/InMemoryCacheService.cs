using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Services
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public Task<string> GetCacheValueAsync(string key)
        {
            return Task.FromResult(_cache.Get<string>(key));
        }

        public Task InValidateKey(string keyPrefix)
        {
            return Task.CompletedTask;
        }

        public Task SetCacheValueAsync(string key, string value)
        {
            _cache.Set(key, value);
            return Task.CompletedTask;
        }

        public Task SetCacheValueAsync(string key, string value, TimeSpan timeSpan)
        {
            _cache.Set(key, value, timeSpan);
            return Task.CompletedTask;
        }
    }
}
