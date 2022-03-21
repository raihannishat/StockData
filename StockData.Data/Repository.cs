using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace StockData.Data
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : class, IEntity<TKey>
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void AddAll(IList<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }
    }
}
