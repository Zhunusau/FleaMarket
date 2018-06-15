using System;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChanges();
        void RollBack();
    }
}
