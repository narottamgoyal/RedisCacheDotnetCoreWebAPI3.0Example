using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
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

        public async Task SetCacheValueAsync(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null) return;
            await SetCacheValueAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task SetCacheValueAsync(string key, object value, TimeSpan timeSpan)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null) return;
            await SetCacheValueAsync(key, JsonConvert.SerializeObject(value), timeSpan);
        }
    }
}
