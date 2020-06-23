using System;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Services
{
    public interface ICacheService
    {
        Task InValidateKey(string keyPrefix);
        Task<string> GetCacheValueAsync(String key);
        Task SetCacheValueAsync(String key, String value);
        Task SetCacheValueAsync(String key, object value);
        Task SetCacheValueAsync(string key, object value, TimeSpan timeSpan);

        Task SetCacheValueAsync(string key, string value, TimeSpan timeSpan);
    }
}
