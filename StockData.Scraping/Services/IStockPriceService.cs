using StockData.Scraping.BusinessObjects;
using System.Collections.Generic;

namespace StockData.Scraping.Services
{
    public interface IStockPriceService
    {
        void CreateAllStockPrices(IList<StockPrice> stockPrices);
    }
}