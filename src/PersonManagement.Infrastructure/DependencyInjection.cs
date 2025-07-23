using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Application.Caching;
using PersonManagement.Infrastructure.Caching;
using StackExchange.Redis;

namespace PersonManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}
