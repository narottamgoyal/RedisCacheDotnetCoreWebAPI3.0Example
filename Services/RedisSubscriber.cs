using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RedisCacheWebAPIExample.Services
{
    public class RedisSubscriber : BackgroundService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisSubscriber(IConnectionMultiplexer connectionMultiplexer)
        {
            this._connectionMultiplexer = connectionMultiplexer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subs = _connectionMultiplexer.GetSubscriber();
            // "*" means subscribe to all
            return subs.SubscribeAsync("messages", (channel, value) =>
            {
                Console.WriteLine($"Message: {value}");
            });
            //return Task.CompletedTask;
        }
    }
}
