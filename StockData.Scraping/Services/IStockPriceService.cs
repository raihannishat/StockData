using StockData.Scraping.BusinessObjects;
using System.Collections.Generic;

namespace StockData.Scraping.Services
{
    public interface IStockPriceService
    {
        void CreateAllStockPrices(IList<StockPrice> stockPrices);
        IList<StockPrice> GetAllStockPrices(string tradecode);

        (IList<StockPrice> records, int total, int totalDisplay) 
            GetAllStockPrices(int pageIndex, 
            int pageSize,
            string searchText,
            string sortText, 
            string tradecode);
    }
}