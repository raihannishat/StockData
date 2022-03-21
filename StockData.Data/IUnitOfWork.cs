using System;

namespace StockData.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
