using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCacheWebAPIExample.Services;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Controllers
{
    /// <summary>
    /// Direct Testing Controller
    /// </summary>
    [EnableCors("corspolicy")]
    [ApiController]
    [Route("[controller]")]
    public class DirectTestingController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheService"></param>
        public DirectTestingController(ICacheService cacheService)
        {
            _cacheService = cacheService;
            _cacheService.SetCacheValueAsync("Cache Name", cacheService.GetType().Name);
        }

        /// <summary>
        /// Return cached data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet("{key}")]
        public async Task<IActionResult> GetAsync(string key)
        {
            var result = await _cacheService.GetCacheValueAsync(key);
            if (result == null) return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Cache data with key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
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
        /// Return cache db name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _cacheService.GetCacheValueAsync("Cache Name");
            if (result == null) return StatusCode(StatusCodes.Status404NotFound);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
