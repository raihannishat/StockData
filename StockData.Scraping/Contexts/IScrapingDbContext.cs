using Microsoft.EntityFrameworkCore;
using StockData.Scraping.Entities;

namespace StockData.Scraping.Contexts
{
    public interface IScrapingDbContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<StockPrice> StockPrices { get; set; }
    }
}