using HtmlAgilityPack;
using StockData.Scraping.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Scraping.Services
{
    public class ScrapingService : IScrapingService, IDisposable
    {
        private HtmlWeb _web;
        private HtmlDocument _document;
        private ICompanyService _companyService;
        private IStockPriceService _stockPriceService;
        private string _url;
        private HtmlNode[] _nodes;
        private string _currentStatus;
        private IList<Company> _companies;
        private IList<StockPrice> _stockPrices;

        public ScrapingService(ICompanyService companyService, IStockPriceService stockPriceService)
        {
            _companyService = companyService;
            _stockPriceService = stockPriceService;
        }

        public void SaveStockData()
        {
            _url = "https://www.dse.com.bd/latest_share_price_scroll_l.php";
            _web = new HtmlWeb();
            _document = _web.Load(_url);
            _currentStatus = _document.DocumentNode
                                .SelectSingleNode("//div[@class='HeaderTop']/span[@class='time']/span[@class='green']")
                                .InnerText;
            _nodes = _document.DocumentNode
                        .SelectNodes("//table[@class='table table-bordered background-white shares-table fixedHeader']")
                        .ToArray();

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
            foreach (var node in _nodes)
            {
                var stockInfo = node.SelectNodes(".//tr/td");
                _companies = new List<Company>();
                _stockPrices = new List<StockPrice>();

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
                        TradeCode = company.TradeCode,
                        LastTradingPrice = stockInfo[i + 2].InnerText.Trim(),
                        High = stockInfo[i + 3].InnerText.Trim(),
                        Low = stockInfo[i + 4].InnerText.Trim(),
                        ClosePrice = stockInfo[i + 5].InnerText.Trim(),
                        YesterdayClosePrice = stockInfo[i + 6].InnerText.Trim(),
                        Change = stockInfo[i + 7].InnerText.Trim(),
                        Trade = stockInfo[i + 8].InnerText.Trim(),
                        Value = stockInfo[i + 9].InnerText.Trim(),
                        Volume = stockInfo[i + 10].InnerText.Trim(),
                        Time = DateTime.Now.ToString(),
                    };

                    _companies.Add(company);
                    _stockPrices.Add(stockPrice);
                }
            }

            return (_companies, _stockPrices);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
