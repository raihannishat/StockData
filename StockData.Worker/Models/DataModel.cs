using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace StockData.Worker.Models
{
    public class DataModel
    {
        public string CurrentStatus 
        {
            get
            {
                return _document.DocumentNode
                    .SelectSingleNode("//div[@class='HeaderTop']/span[@class='time']/span[@class='green']")
                    .InnerText;
            }
        }

        public DataModel()
        {
            _url = "https://www.dse.com.bd/latest_share_price_scroll_l.php";
            _web = new HtmlWeb();
            _document = _web.Load(_url);
            _dataList = new List<Data>();
            _nodes = _document.DocumentNode
                .SelectNodes("//table[@class='table table-bordered background-white shares-table fixedHeader']")
                .ToArray();
        }

        private readonly string _url;
        private readonly HtmlWeb _web;
        private readonly HtmlDocument _document;
        private readonly HtmlNode[] _nodes;
        private readonly IList<Data> _dataList;

        public IList<Data> GetAllData()
        {
            foreach (var node in _nodes)
            {
                var stockInfo = node.SelectNodes(".//tr/td");

                for (int i = 0; i < stockInfo.Count; i += 11)
                {
                    _dataList.Add(
                        new Data
                        {
                            Id = int.Parse(stockInfo[i].InnerText.Trim()),
                            TradingCode = stockInfo[i + 1].InnerText.Trim(),
                            LastTradingPrice = stockInfo[i + 2].InnerText.Trim(),
                            High = stockInfo[i + 3].InnerText.Trim(),
                            Low = stockInfo[i + 4].InnerText.Trim(),
                            ClosePrice = stockInfo[i + 5].InnerText.Trim(),
                            YesterdayClosePrice = stockInfo[i + 6].InnerText.Trim(),
                            Change = stockInfo[i + 7].InnerText.Trim(),
                            Trade = stockInfo[i + 8].InnerText.Trim(),
                            Value = stockInfo[i + 9].InnerText.Trim(),
                            Volume = stockInfo[i + 10].InnerText.Trim(),
                        }
                    );
                }
            }

            return _dataList;
        }
    }
}
