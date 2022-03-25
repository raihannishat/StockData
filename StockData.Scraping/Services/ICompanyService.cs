using StockData.Scraping.BusinessObjects;
using System.Collections.Generic;

namespace StockData.Scraping.Services
{
    public interface ICompanyService
    {
        void CreateAllCompanies(IList<Company> companies);
        (IList<Company> records, int total, int totalDisplay) 
            GetAllCompanies(int pageIndex, int pageSize, string searchText, string sortText);
    }
}