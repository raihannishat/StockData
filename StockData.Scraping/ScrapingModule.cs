using Autofac;
using StockData.Scraping.Contexts;
using StockData.Scraping.Services;
using StockData.Scraping.Repositories;
using StockData.Scraping.UnitOfWorks;

namespace StockData.Scraping
{
    public class ScrapingModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ScrapingModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScrapingDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<ScrapingDbContext>().As<IScrapingDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssembly", _migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockPriceRepository>().As<IStockPriceRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyService>().As<ICompanyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StockPriceService>().As<IStockPriceService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ScrapingUnitOfWork>().As<IScrapingUnitOfWork>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
