using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Services
{
    public interface ICacheService
    {
        public Task<string> GetCacheValueAsync(String key);
        public Task SetCacheValueAsync(String key, String value);
    }
}
