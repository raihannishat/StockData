using Autofac;
using StockData.Scraping.BusinessObjects;
using StockData.Scraping.Services;
using System.Collections.Generic;
using System.Linq;

namespace StockData.Web.Models
{
    public class StockPriceModel
    {
        public IList<StockPrice> StockPrices = new List<StockPrice>();
        private IStockPriceService _stockPriceService;

        public string TradeCode { get; set; }

        public StockPriceModel(ILifetimeScope scope)
        {
            _stockPriceService = scope.Resolve<IStockPriceService>();
        }

        public object GetStockPrices(DataTablesAjaxRequestModel dataTablesModel, string tradecode)
        {
            var data = _stockPriceService.GetAllStockPrices(
                dataTablesModel.PageIndex,
                dataTablesModel.PageSize,
                dataTablesModel.SearchText, "id desc", tradecode);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                              record.Id.ToString(),
                              record.LastTradingPrice.ToString(),
                              record.High.ToString(),
                              record.Low.ToString(),
                              record.ClosePrice.ToString(),
                              record.YesterdayClosePrice.ToString(),
                              record.Change.ToString(),
                              record.Trade.ToString(),
                              record.Value.ToString(),
                              record.Volume.ToString(),
                              record.Time.ToString()
                        }
                ).ToArray()
            };
        }
    }
}
