using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponse(string key, object value, TimeSpan timeSpan);
        Task<string> GetResponse(string key);
    }

    public class ResponseCacheService : IResponseCacheService
    {
        private readonly ICacheService _cacheService;
        public ResponseCacheService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task CacheResponse(string key, object value, TimeSpan timeSpan)
        {
            if (string.IsNullOrWhiteSpace(key) || value == null) return;
            await _cacheService.SetCacheValueAsync(key, JsonConvert.SerializeObject(value), timeSpan);
        }

        public async Task<string> GetResponse(string key)
        {
            return await _cacheService.GetCacheValueAsync(key);
        }
    }
}
