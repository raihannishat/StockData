using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Data;
using StockData.Scraping.Repositories;
using StockData.Scraping.Contexts;

namespace StockData.Scraping.UnitOfWorks
{
    public class ScrapingUnitOfWork : UnitOfWork, IScrapingUnitOfWork
    {
        public ScrapingUnitOfWork(IScrapingDbContext dbContext,
            ICompanyRepository companyRepository,
            IStockPriceRepository stockPriceRepository)
            : base((Context)dbContext)
        {
            CompanyRepository = companyRepository;
            StockPriceRepository = stockPriceRepository;
        }

        public ICompanyRepository CompanyRepository { get; private set; }
        public IStockPriceRepository StockPriceRepository { get; private set; }
    }
}
