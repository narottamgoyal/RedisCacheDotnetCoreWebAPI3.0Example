using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RedisCacheWebAPIExample.Services;
using StackExchange.Redis;

namespace RedisCacheWebAPIExample.Config
{
    /// <summary>
    /// Start extension class
    /// </summary>
    public static class StartupConfig
    {
        /// <summary>
        /// Load app settings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ConfigurationSetting RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationSetting>(configuration.GetSection("Configuration"));
            var serviceProvider = services.BuildServiceProvider();
            var iop = serviceProvider.GetService<IOptions<ConfigurationSetting>>();
            return iop.Value;
        }

        /// <summary>
        /// Register Service Dependancies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationSetting"></param>
        public static void RegisterServiceDependancies(this IServiceCollection services, ConfigurationSetting configurationSetting)
        {
            if (configurationSetting.UseRedisCache)
            {
                services.AddSingleton<ICacheService, RedisCacheService>();
            }
            else
                services.AddSingleton<ICacheService, InMemoryCacheService>();
        }

        /// <summary>
        /// Register Db Dependancies
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationSetting"></param>
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
