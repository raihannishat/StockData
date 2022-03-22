using StockData.Scraping.BusinessObjects;
using System;
using System.Collections.Generic;

namespace StockData.Scraping.Services
{
    public interface IScrapingService : IDisposable
    {
        void SaveStockData();
    }
}