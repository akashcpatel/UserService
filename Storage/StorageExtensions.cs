using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Connector.SqlServer.EFCore;
using Storage.Implementations;
using System;

namespace Storage
{
    public static class StorageExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<StorageConfig>(config, StorageConfig.PositionInConfig);

            services.AddDbContext<UserDataContext>(options => options.UseSqlServer(config), ServiceLifetime.Singleton);

            services.RegisterServices();
            return services;
        }

        public static void InitializeMyContexts(this IServiceProvider serviceProvider)
        {
            using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var db = serviceScope.ServiceProvider.GetService<UserDataContext>();
            db.Database.EnsureCreated();
            db.SaveChanges();
        }

        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHealthContributor, StorageHealthContributor>();

            return services;
        }
    }
}
