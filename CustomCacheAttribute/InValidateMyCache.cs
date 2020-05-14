using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RedisCacheWebAPIExample.Services;
using System;

namespace RedisCacheWebAPIExample.CustomCacheAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class InValidateMyCache : Attribute, IActionFilter
    {
        private string _path;

        public InValidateMyCache(string path)
        {
            _path = path;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var responseCacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            responseCacheService.InValidateKey(_path);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
