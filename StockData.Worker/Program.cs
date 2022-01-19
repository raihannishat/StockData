using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using StockData.Scraping;
using StockData.Scraping.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace StockData.Worker
{
    public class Program
    {
        private static IConfiguration _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

        private static string _connectionString = _configuration
            .GetConnectionString("DefaultConnection");

        private static string _migrationAssembly = Assembly.GetExecutingAssembly()
            .GetName().FullName;

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();

            try
            {
                Log.Information("Worker Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Worker start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureContainer<ContainerBuilder>(builder => {
                    builder.RegisterModule(new ScrapingModule(_connectionString,
                        _migrationAssembly));

                    builder.RegisterModule(new WorkerModule(_configuration,
                        _connectionString,
                        _migrationAssembly));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                    services.AddHostedService<Worker>();

                    services.AddDbContext<ScrapingDbContext>(options =>
                        options.UseSqlServer(_connectionString,
                        m => m.MigrationsAssembly(_migrationAssembly)));
                });
    }
}
