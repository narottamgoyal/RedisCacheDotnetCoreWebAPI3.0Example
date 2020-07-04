using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCacheWebAPIExample.CustomCacheAttribute;
using RedisCacheWebAPIExample.Services;
using System;

namespace RedisCacheWebAPIExample.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    [EnableCors("corspolicy")]
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        /// <summary>
        /// HomeController
        /// </summary>
        /// <param name="cacheService"></param>
        public HomeController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        /// <summary>
        /// Return cached data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("{key}")]
        [MyCache(60)]
        public IActionResult Get(string key)
        {
            return StatusCode(StatusCodes.Status200OK, DateTime.Now.ToString());
        }

        /// <summary>
        /// Invalidate cache on post
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [InValidateMyCache(@"[\/]home")]
        public IActionResult Post()
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Invalidate cache on update
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [InValidateMyCache(@"[\/]home")]
        public IActionResult Put()
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Invalidate cache on delete
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [InValidateMyCache(@"[\/]home")]
        public IActionResult Delete()
        {
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
