using System;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Services
{
    public interface ICacheService
    {
        Task<string> GetCacheValueAsync(String key);
        Task SetCacheValueAsync(String key, String value);
        Task SetCacheValueAsync(string key, string value, TimeSpan timeSpan);
    }
}
