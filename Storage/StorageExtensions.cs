using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Common.HealthChecks;
using Steeltoe.Connector.SqlServer.EFCore;
using Storage.Implementations;

namespace Storage
{
    public static class StorageExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration config)
        {
            services.AddConfig<StorageConfig>(config, StorageConfig.PositionInConfig);

            services.AddDatabase(config);

            services.RegisterServices();
            return services;
        }

        private static void AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<UserDataContext>(options => options.UseSqlServer(config), ServiceLifetime.Singleton);

            var db = services.BuildServiceProvider().GetService<UserDataContext>();
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
