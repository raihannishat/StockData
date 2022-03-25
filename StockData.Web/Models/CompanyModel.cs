using Autofac;
using StockData.Scraping.BusinessObjects;
using StockData.Scraping.Services;
using System.Collections.Generic;
using System.Linq;

namespace StockData.Web.Models
{
    public class CompanyModel
    {
        public IList<Company> Companies = new List<Company>();
        private ICompanyService _companyService;

        public CompanyModel(ILifetimeScope scope)
        {
            _companyService = scope.Resolve<ICompanyService>();
        }

        public object GetCompanies(DataTablesAjaxRequestModel dataTablesModel)
        {
            var data = _companyService.GetAllCompanies(
                dataTablesModel.PageIndex,
                dataTablesModel.PageSize,
                dataTablesModel.SearchText,
                dataTablesModel.GetSortText(new string[] { "Id", "TradeCode" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Id.ToString(),
                            record.TradeCode.ToString(),
                            record.TradeCode.ToString(),
                        }
                ).ToArray()
            };
        }
    }
}
