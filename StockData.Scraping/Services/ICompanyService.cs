using StockData.Scraping.BusinessObjects;
using System.Collections.Generic;

namespace StockData.Scraping.Services
{
    public interface ICompanyService
    {
        void CreateAllCompanies(IList<Company> companies);
    }
}