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
    public class StockPriceService : IStockPriceService
    {
        private readonly IScrapingUnitOfWork _scrapingUnitOfWork;
        private readonly IMapper _mapper;

        public StockPriceService(IScrapingUnitOfWork scrapingUnitOfWork, IMapper mapper)
        {
            _scrapingUnitOfWork = scrapingUnitOfWork;
            _mapper = mapper;
        }

        public void CreateAllStockPrices(IList<StockPrice> stockPrices)
        {
            var myStockPrices = new List<Entities.StockPrice>();

            foreach (var stockPrice in stockPrices)
            {
                myStockPrices.Add(_mapper.Map<Entities.StockPrice>(stockPrice));
            }

            _scrapingUnitOfWork.StockPriceRepository.AddAll(myStockPrices);
            _scrapingUnitOfWork.Save();
        }

        public IList<StockPrice> GetAllStockPrices(string tradecode)
        {
            var myStockPrices = new List<StockPrice>();

            foreach (var price in _scrapingUnitOfWork
                .StockPriceRepository
                .GetAll(x => x.TradeCode == tradecode)
                .OrderByDescending(y => y.Id))
            {
                myStockPrices.Add(_mapper.Map<StockPrice>(price));
            }

            return myStockPrices;
        }

        public (IList<StockPrice> records, int total, int totalDisplay) 
            GetAllStockPrices(int pageIndex, int pageSize, string searchText, string sortText, string tradecode)
        {
            var filter = string.IsNullOrWhiteSpace(searchText) ? string.Empty : searchText.Trim();

            var stockPriceData = _scrapingUnitOfWork.StockPriceRepository.GetDynamic(
                x => x.TradeCode == tradecode && (x.Time.Contains(filter) ||
                x.LastTradingPrice.Contains(filter) ||
                x.Value.Contains(filter) ||
                x.Volume.Contains(filter)),
                sortText, string.Empty, pageIndex, pageSize);

            var result = (from stockPrice in stockPriceData.data
                          select new StockPrice
                          {
                              Id = stockPrice.Id,
                              CompanyId = stockPrice.CompanyId,
                              TradeCode = stockPrice.TradeCode,
                              LastTradingPrice = stockPrice.LastTradingPrice,
                              High = stockPrice.High,
                              Low = stockPrice.Low,
                              ClosePrice = stockPrice.ClosePrice,
                              YesterdayClosePrice = stockPrice.YesterdayClosePrice,
                              Change = stockPrice.Change,
                              Trade = stockPrice.Trade,
                              Value = stockPrice.Value,
                              Volume = stockPrice.Volume,
                              Time = stockPrice.Time
                          }).ToList();

            return (result, stockPriceData.total, stockPriceData.totalDisplay);
        }
    }
}
