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
        [MyCache(120)]
        public async Task<IActionResult> GetAsync(string key)
        {
            return StatusCode(StatusCodes.Status200OK, DateTime.Now.ToString());
        }

        [HttpPost]
        [InValidateMyCache(@"[\/]home")]
        public async Task<IActionResult> Post()
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut]
        [InValidateMyCache(@"[\/]home")]
        public async Task<IActionResult> Put()
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete]
        [InValidateMyCache(@"[\/]home")]
        public async Task<IActionResult> Delete()
        {
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
