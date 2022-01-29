using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publisher.Implementations;
using Publisher.Implementations.AsyncCommunicators;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Connector.RabbitMQ;

namespace Publisher
{
    public static class PublisherExtensions
    {
        public static IServiceCollection AddPublisher(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<PublisherConfig>(config, PublisherConfig.PositionInConfig);            
            services.AddRabbitMQConnection(config);

            services.RegisterServices();
            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserChangedPublisher, UserChangedPublisher>();
            services.AddScoped<IAsyncCommunicator, RabbitMQCommunicator>();
            services.AddScoped<IHealthContributor, PublisherHealthContributor>();

            return services;
        }
    }
}
