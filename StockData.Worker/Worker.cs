using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StockData.Scraping.Services;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        public ILifetimeScope AutofacContainer { get; private set; }
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger, ILifetimeScope lifetimeScope)
        {
            _logger = logger;
            AutofacContainer = lifetimeScope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var scrapingService = AutofacContainer.Resolve<IScrapingService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    scrapingService.SaveStockData();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, DateTimeOffset.Now.ToString());
                }

                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
