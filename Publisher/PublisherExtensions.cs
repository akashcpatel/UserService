using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Connector.RabbitMQ;

namespace Publisher
{
    public static class PublisherExtensions
    {
        public static IServiceCollection AddPublisher(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<PublisherConfig>(config, PublisherConfig.PositionInConfig);
            services.AddHealthChecks().AddCheck<PublisherHealthCheck>(nameof(PublisherHealthCheck));

            services.AddRabbitMQConnection(config);

            services.RegisterServices();
            return services;
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserChangedPublisher, Implementations.UserChangedPublisher>();

            return services;
        }
    }
}
