using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models;

namespace GodelMastery.FleaMarket.DAL.Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IBaseEntity<TKey>
    {
        private readonly DbSet<TEntity> objectSet;

        public BaseRepository(DbContext dbContext)
        {
            objectSet = dbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll => objectSet;

        public virtual void Create(TEntity entity) => objectSet.Add(entity);

        public virtual void Update(TEntity entity) => objectSet.AddOrUpdate(entity);

        public virtual void Delete(TEntity entity) => objectSet.Remove(entity);

        public virtual TEntity GetById(TKey id) => objectSet.Find(id);

        public virtual TEntity SingleOrDefault(Func<TEntity, bool> predicate) => objectSet.SingleOrDefault(predicate);

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => objectSet.Where(predicate);

    }
}