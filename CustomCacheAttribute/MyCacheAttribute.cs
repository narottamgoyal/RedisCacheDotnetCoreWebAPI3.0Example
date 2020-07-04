using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheWebAPIExample.Services;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.CustomCacheAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class MyCacheAttribute : Attribute, IAsyncActionFilter
    {
        private int _shouldExpireInSeconds;

        public MyCacheAttribute(int shouldExpireInSeconds)
        {
            _shouldExpireInSeconds = shouldExpireInSeconds;
        }

        /// <summary>
        /// Return cached data if available otherwise wait and cache once executed 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheKey = GetCacheKey(context.HttpContext.Request);
            var cacheResponse = await responseCacheService.GetCacheValueAsync(cacheKey);

            if (!string.IsNullOrWhiteSpace(cacheResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cacheResponse,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.OK
                };
                context.Result = contentResult;
                return;
            }

            var executedContext = await next();
            if (executedContext.Result is ObjectResult okObjectResult)
            {
                await responseCacheService.SetCacheValueAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_shouldExpireInSeconds));
            }
        }

        private string GetCacheKey(HttpRequest request)
        {
            var sb = new StringBuilder();
            sb.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
                sb.Append($"({key},{value})");
            return sb.ToString();
        }
    }
}