using StockData.Scraping.BusinessObjects;

namespace StockData.Scraping.Services
{
    public interface ICompanyService
    {
        void CreateCompany(Company company);
    }
}