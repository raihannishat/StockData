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

        public void CreateAllCompanies(IList<Company> companies)
        {
            var myCompanies = new List<Entities.Company>();

            foreach (var company in companies)
            {
                myCompanies.Add(_mapper.Map<Entities.Company>(company));
            }

            _scrapingUnitOfWork.CompanyRepository.AddAll(myCompanies);
            _scrapingUnitOfWork.Save();
        }
    }
}
