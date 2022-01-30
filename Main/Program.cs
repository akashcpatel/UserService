using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Logging;
using Storage;
using System;
using System.Threading.Tasks;

namespace Main
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //using var scope = host.Services.CreateScope();

            //var serviceProvider = scope.ServiceProvider;
            //try
            //{
            //    if (serviceProvider == null)
            //        throw new ArgumentNullException("serviceProvider");

            //    serviceProvider.InitializeMyContexts();
            //}
            //catch (Exception ex)
            //{
            //    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            //    logger.LogError(ex, "An error occurred seeding the DB.");
            //}

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(ConfigureApp)
            .ConfigureLogging((context, loggingBuilder) => 
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
                loggingBuilder.AddDynamicConsole();
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

        private static void ConfigureApp(HostBuilderContext hostBuilder, IConfigurationBuilder configBuilder)
        {
            configBuilder.AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostBuilder.HostingEnvironment}.json", true, true)
                .AddEnvironmentVariables("USERSERVICE_");

            if (hostBuilder.HostingEnvironment.IsDevelopment())
                configBuilder.AddUserSecrets<Program>();
        }
    }
}
