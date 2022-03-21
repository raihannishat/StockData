using System;

namespace StockData.Data
{
    public interface IEntity<T>
    {   
        T Id { get; set; }  
    }
}
