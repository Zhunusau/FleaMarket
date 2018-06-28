using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GodelMastery.FleaMarket.DAL.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;
        private readonly ILifetimeScope lifetimeScope;
        private bool disposed = false;
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<ApplicationRole> RoleManager { get; }

        public UnitOfWork(DbContext dbContext, ILifetimeScope lifetimeScope)
        {
            this.dbContext = dbContext;
            this.lifetimeScope = lifetimeScope;
            UserManager = lifetimeScope
            .Resolve<UserManager<ApplicationUser>>(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(UserStore<ApplicationUser>),
                (pi, ctx) => ctx.Resolve<IUserStore<ApplicationUser>>(new TypedParameter(typeof(DbContext), dbContext))));
            RoleManager =  lifetimeScope
            .Resolve<RoleManager<ApplicationRole>>(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(RoleStore<ApplicationRole>),
                (pi, ctx) => ctx.Resolve<IRoleStore<ApplicationRole, string>>(new TypedParameter(typeof(DbContext), dbContext))));
        }

        public IBaseRepository<Filter> Filters => GetRepository<IBaseRepository<Filter>>();

        public IBaseRepository<Lot> Lots => GetRepository<IBaseRepository<Lot>>();

        public async Task SaveChanges() => await dbContext.SaveChangesAsync();

        public void RollBack()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private T GetRepository<T>() where T : class
        {
            T repository = lifetimeScope.Resolve<T>();
            return repository;
        }
    }
}
