using Infrastructure.Data;
using Infrastructure.Data.SeedingData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API
{
    internal class Program
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var factoryLogger = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = services.GetRequiredService<StoreContext>();
                await SeedData.SeedDataContext(context, factoryLogger);
            }
            catch (Exception e)
            {
                var logger = factoryLogger.CreateLogger<Program>();
                logger.LogError(e, "Something went wrong while migrating");
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
