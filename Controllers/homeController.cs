using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCacheWebAPIExample.Services;
using StackExchange.Redis;
using System.Collections.Generic;
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
            _cacheService.SetCacheValueAsync("Cache Name", cacheService.GetType().Name);
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(string key)
        {
            var result = await _cacheService.GetCacheValueAsync(key);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost("{key}/{value}")]
        public async Task<IActionResult> Post(string key, string value)
        {
            await _cacheService.SetCacheValueAsync(key, value);
            return StatusCode(StatusCodes.Status201Created);
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
            var result = await _cacheService.GetCacheValueAsync("Cache Name");
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
