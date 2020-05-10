using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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

        public static void RegisterServiceDependancies(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IMongoDbQueryRepository, MongoDbQueryRepository>();
        }

        public static void RegisterDbDependancies(this IServiceCollection services, ConfigurationSetting configurationSetting)
        {
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configurationSetting.RedisConnectionString);
            services.AddScoped(s => redis.GetDatabase());

            //services.AddSingleton<IConnectionMultiplexer>(x =>
            //ConnectionMultiplexer.Connect(configurationSetting.RedisConnectionString));
        }
    }
}
