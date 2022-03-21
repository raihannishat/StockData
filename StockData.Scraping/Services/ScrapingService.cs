using HtmlAgilityPack;
using StockData.Scraping.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraping.Services
{
    public class ScrapingService : IScrapingService
    {
        private readonly HtmlWeb _web;
        private readonly HtmlDocument _document;
        private readonly ICompanyService _companyService;
        private readonly IStockPriceService _stockPriceService;

        private string _url
        {
            get
            {
                return "https://www.dse.com.bd/latest_share_price_scroll_l.php";
            }
        }

        private HtmlNode[] _nodes 
        {
            get
            {
                return _document.DocumentNode
                .SelectNodes("//table[@class='table table-bordered background-white shares-table fixedHeader']")
                .ToArray();
            }
        }

        private string _currentStatus 
        {
            get
            {
                return _document.DocumentNode
                    .SelectSingleNode("//div[@class='HeaderTop']/span[@class='time']/span[@class='green']")
                    .InnerText; ;
            }
        }

        public ScrapingService(ICompanyService companyService, IStockPriceService stockPriceService)
        {
            _web = new HtmlWeb();
            _document = _web.Load(_url);
            _companyService = companyService;
            _stockPriceService = stockPriceService;
        }

        public void SaveStockData()
        {
            if (_currentStatus.Equals("Open"))
            {
                try
                {
                    _stockPriceService.CreateAllStockPrices(GetAllCompanyAndStockPrice().StockPrices);
                }
                catch
                {
                    _companyService.CreateAllCompanies(GetAllCompanyAndStockPrice().Companies);
                }
            }
        }

        private (IList<Company> Companies, IList<StockPrice> StockPrices) GetAllCompanyAndStockPrice()
        {
            var companies = new List<Company>();
            var stockPrices = new List<StockPrice>();

            foreach (var node in _nodes)
            {
                var stockInfo = node.SelectNodes(".//tr/td");

                for (int i = 0; i < stockInfo.Count; i += 11)
                {
                    var company = new Company
                    {
                        Id = int.Parse(stockInfo[i].InnerText.Trim()),
                        TradeCode = stockInfo[i + 1].InnerText.Trim()
                    };

                    var stockPrice = new StockPrice
                    {
                        CompanyId = company.Id,
                        LastTradingPrice = stockInfo[i + 2].InnerText.Trim(),
                        High = stockInfo[i + 3].InnerText.Trim(),
                        Low = stockInfo[i + 4].InnerText.Trim(),
                        ClosePrice = stockInfo[i + 5].InnerText.Trim(),
                        YesterdayClosePrice = stockInfo[i + 6].InnerText.Trim(),
                        Change = stockInfo[i + 7].InnerText.Trim(),
                        Trade = stockInfo[i + 8].InnerText.Trim(),
                        Value = stockInfo[i + 9].InnerText.Trim(),
                        Volume = stockInfo[i + 10].InnerText.Trim()
                    };

                    companies.Add(company);
                    stockPrices.Add(stockPrice);
                }
            }

            return (companies, stockPrices);
        }
    }
}
