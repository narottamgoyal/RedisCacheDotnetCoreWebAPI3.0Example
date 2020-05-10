using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Collections.Generic;

namespace RedisCacheWebAPIExample.Controllers
{
    [EnableCors("corspolicy")]
    [ApiController]
    [Route("[controller]")]
    public class homeController : ControllerBase
    {
        private readonly IDatabase _database;

        public homeController(IDatabase database)
        {
            _database = database;
        }

        [HttpGet("{key}")]
        public string Get(string key)
        {
            return _database.StringGet(key);
        }

        [HttpPost]
        public void Post([FromBody] KeyValuePair<string, string> keyValue)
        {
            _database.StringSet(keyValue.Key, keyValue.Value);
        }

        /// <summary>
        /// Home controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status200OK, "Its working");
        }
    }
}
