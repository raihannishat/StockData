using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Data;
using StockData.Scraping.Repositories;

namespace StockData.Scraping.UnitOfWorks
{
    public interface IScrapingUnitOfWork : IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        IStockPriceRepository StockPriceRepository { get; }
    }
}
