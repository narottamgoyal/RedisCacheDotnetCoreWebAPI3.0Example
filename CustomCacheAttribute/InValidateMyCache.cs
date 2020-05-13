using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace RedisCacheWebAPIExample.CustomCacheAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class InValidateMyCache : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
