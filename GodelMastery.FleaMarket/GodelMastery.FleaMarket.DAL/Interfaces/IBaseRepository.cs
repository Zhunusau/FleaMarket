using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GodelMastery.FleaMarket.DAL.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll { get; }
        TEntity GetById<TKey>(TKey id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity SingleOrDefault(Func<TEntity, bool> predicate);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
