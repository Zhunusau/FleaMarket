using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GodelMastery.FleaMarket.DAL.Models;

namespace GodelMastery.FleaMarket.DAL.Interfaces
{
    public interface IBaseRepository<TEntity, in TKey> 
        where TEntity : IBaseEntity<TKey>
    {
        IEnumerable<TEntity> GetAll { get; }
        TEntity GetById(TKey id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        TEntity SingleOrDefault(Func<TEntity, bool> predicate);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
