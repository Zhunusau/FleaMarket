using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.DAL.Interfaces;

namespace GodelMastery.FleaMarket.DAL.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;
        private bool disposed = false;

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
    }
}
