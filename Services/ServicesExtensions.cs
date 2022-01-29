﻿using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Publisher;
using Services.Implementations;
using Steeltoe.Common.HealthChecks;
using Storage;

namespace Services
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<ServicesConfig>(config, ServicesConfig.PositionInConfig);
            services.AddPublisher(config);
            services.AddStorage(config);
            services.RegisterServices();

            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHealthContributor, ServicesHealthContributor>();
        }
    }
}
