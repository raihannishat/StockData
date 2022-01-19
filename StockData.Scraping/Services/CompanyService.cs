using StockData.Scraping.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Scraping.UnitOfWorks;
using AutoMapper;

namespace StockData.Scraping.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IScrapingUnitOfWork _scrapingUnitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IScrapingUnitOfWork scrapingUnitOfWork, IMapper mapper)
        {
            _scrapingUnitOfWork = scrapingUnitOfWork;
            _mapper = mapper;
        }

        public void CreateCompany(Company company)
        {
            _scrapingUnitOfWork.CompanyRepository.Add(
                _mapper.Map<Entities.Company>(company));

            _scrapingUnitOfWork.Save();
        }

        public Company GetByTradeCode(string tradeCode)
        {
            return _mapper.Map<Company>(_scrapingUnitOfWork.CompanyRepository
                .Get(x => x.TradeCode == tradeCode, string.Empty)
                .FirstOrDefault());
        }
    }
}
