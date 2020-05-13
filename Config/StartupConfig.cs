using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RedisCacheWebAPIExample.Services;
using StackExchange.Redis;

namespace RedisCacheWebAPIExample.Config
{
    public static class StartupConfig
    {
        public static ConfigurationSetting RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationSetting>(configuration.GetSection("Configuration"));
            var serviceProvider = services.BuildServiceProvider();
            var iop = serviceProvider.GetService<IOptions<ConfigurationSetting>>();
            return iop.Value;
        }

        public static void RegisterServiceDependancies(this IServiceCollection services, ConfigurationSetting configurationSetting)
        {
            if (configurationSetting.UseRedisCache)
            {
                services.AddSingleton<ICacheService, RedisCacheService>();
                services.AddHostedService<RedisSubscriber>();
            }
            else
                services.AddSingleton<ICacheService, InMemoryCacheService>();
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }

        public static void RegisterDbDependancies(this IServiceCollection services, ConfigurationSetting configurationSetting)
        {
            //IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configurationSetting.RedisConnectionString);
            //services.AddScoped(s => redis.GetDatabase());
            if (configurationSetting.UseRedisCache)
            {
                services.AddSingleton<IConnectionMultiplexer>(x =>
                    ConnectionMultiplexer.Connect(configurationSetting.RedisConnectionString));
            }
        }
    }
}
