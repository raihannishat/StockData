using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Data;
using StockData.Scraping.Entities;

namespace StockData.Scraping.Repositories
{
    public interface ICompanyRepository : IRepository<Company, int>
    {
    }
}
