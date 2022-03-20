using AutoMapper;
using Entity = StockData.Scraping.Entities;
using BusinessObject = StockData.Scraping.BusinessObjects;

namespace StockData.Scraping.Dependencies
{
    public class ScrapingProfile : Profile
    {
        public ScrapingProfile()
        {
            CreateMap<Entity.Company, BusinessObject.Company>().ReverseMap();
            CreateMap<Entity.StockPrice, BusinessObject.StockPrice>().ReverseMap();
        }
    }
}
