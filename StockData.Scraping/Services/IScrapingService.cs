using StockData.Scraping.BusinessObjects;
using System.Collections.Generic;

namespace StockData.Scraping.Services
{
    public interface IScrapingService
    {
        void SaveStockData();
    }
}