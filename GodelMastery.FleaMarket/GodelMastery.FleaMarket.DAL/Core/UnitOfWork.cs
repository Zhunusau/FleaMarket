using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;

namespace GodelMastery.FleaMarket.DAL.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;
        private readonly ILifetimeScope lifetimeScope;
        private bool disposed = false;
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<ApplicationRole> RoleManager { get; }

        public UnitOfWork(
            DbContext dbContext, 
            ILifetimeScope lifetimeScope, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.lifetimeScope = lifetimeScope;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public IBaseRepository<Filter> Filters => GetRepository<IBaseRepository<Filter>>();

        public IBaseRepository<Lot> Lots => GetRepository<IBaseRepository<Lot>>();

        public IBaseRepository<UserProfile> UserProfiles => GetRepository<IBaseRepository<UserProfile>>();

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }

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
