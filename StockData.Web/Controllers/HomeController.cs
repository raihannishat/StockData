using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockData.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StockData.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILifetimeScope _scope;

        public HomeController(ILogger<HomeController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetCompaniesData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new CompanyModel(_scope);
            var data = model.GetCompanies(dataTablesModel);
            return Json(data);
        }

        public JsonResult GetStockPricesData(string tradecode)
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = new StockPriceModel(_scope);
            var data = model.GetStockPrices(dataTablesModel, tradecode);
            return Json(data);
        }

        public IActionResult Price(string tradecode)
        {
            var stockPriceModel = new StockPriceModel(_scope);
            stockPriceModel.TradeCode = tradecode;
            return View(stockPriceModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
