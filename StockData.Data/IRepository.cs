using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StockData.Data
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        void AddAll(IList<TEntity> entities);
        IList<TEntity> GetAll();
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
            Expression<Func<TEntity, bool>> filter = null,
            string orderBy = null,
            string includeProperties = "", 
            int pageIndex = 1, 
            int pageSize = 10, 
            bool isTrackingOff = false);
    }
}
