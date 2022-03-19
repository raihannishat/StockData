using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StockData.Data
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        void Add(TEntity entity);
    }
}
