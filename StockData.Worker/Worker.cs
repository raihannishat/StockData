using Autofac;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockData.Worker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockData.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public static ILifetimeScope AutofacContainer { get; private set; }

        public Worker(ILogger<Worker> logger, ILifetimeScope lifetimeScope)
        {
            _logger = logger;
            AutofacContainer = lifetimeScope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var model = new TableModel();
                model.Save();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
