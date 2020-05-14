using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCacheWebAPIExample.CustomCacheAttribute;
using RedisCacheWebAPIExample.Services;
using System;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Controllers
{
    [EnableCors("corspolicy")]
    [ApiController]
    [Route("[controller]")]
    public class homeController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public homeController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("{key}")]
        [MyCache(60)]
        public async Task<IActionResult> GetAsync(string key)
        {
            return StatusCode(StatusCodes.Status200OK, DateTime.Now.ToString());
        }

        [HttpPost("{key}/{value}")]
        [InValidateMyCache(@"[\/]home")]
        public async Task<IActionResult> Post(string key, string value)
        {
            return StatusCode(StatusCodes.Status200OK, DateTime.Now.ToString());
        }

        /// <summary>
        /// Api status check
        /// </summary>
        /// <returns></returns>
        [HttpGet("status")]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status200OK, "Its working");
        }

        /// <summary>
        /// Home controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return StatusCode(StatusCodes.Status200OK, _cacheService.GetType().Name);
        }
    }
}
