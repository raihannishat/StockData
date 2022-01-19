using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Data;
using StockData.Scraping.Entities;
using StockData.Scraping.Contexts;

namespace StockData.Scraping.Repositories
{
    public class CompanyRepository : Repository<Company, int>, ICompanyRepository
    {
        public CompanyRepository(IScrapingDbContext context) : base((Context)context)
        {
        }
    }
}
