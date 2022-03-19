using StockData.Scraping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using StockData.Scraping.BusinessObjects;
using Microsoft.Extensions.Logging;

namespace StockData.Worker.Models
{
    public class TableModel
    {
        private readonly ICompanyService _companyService;
        private readonly IStockPriceService _stockPriceService;
        private readonly DataModel _dataModel; 

        public TableModel()
        {
            _companyService = Worker.AutofacContainer.Resolve<ICompanyService>();
            _stockPriceService = Worker.AutofacContainer.Resolve<IStockPriceService>();
            _dataModel = new DataModel();
        }

        public TableModel(ICompanyService companyService, 
            IStockPriceService stockPriceService,
            DataModel dataModel)
        {
            _companyService = companyService;
            _stockPriceService = stockPriceService;
            _dataModel = dataModel;
        }

        public void Save()
        {
            if (_dataModel.CurrentStatus.Equals("Closed"))
            {
                foreach (var item in _dataModel.GetAllData())
                {
                    var company = new Company
                    {
                        Id = item.Id,
                        TradeCode = item.TradingCode
                    };

                    var stockPrice = new StockPrice
                    {
                        CompanyId = company.Id,
                        LastTradingPrice = item.LastTradingPrice,
                        High = item.High,
                        Low = item.Low,
                        ClosePrice = item.ClosePrice,
                        YesterdayClosePrice = item.YesterdayClosePrice,
                        Change = item.Change,
                        Trade = item.Trade,
                        Value = item.Value,
                        Volume = item.Volume,
                    };

                    try
                    {
                        _stockPriceService.CreateStockPrice(stockPrice);
                    }
                    catch
                    {
                        _companyService.CreateCompany(company);
                    }
                }
            }
        }
    }
}
