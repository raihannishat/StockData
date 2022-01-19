using StockData.Scraping.BusinessObjects;

namespace StockData.Scraping.Services
{
    public interface IStockPriceService
    {
        void CreateStockPrice(StockPrice stockPrice);
    }
}