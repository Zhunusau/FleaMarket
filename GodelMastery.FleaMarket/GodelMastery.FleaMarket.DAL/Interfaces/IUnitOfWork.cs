using System;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;

namespace GodelMastery.FleaMarket.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        RoleManager<ApplicationRole> RoleManager { get; }
        UserManager<ApplicationUser> UserManager { get; }
        IBaseRepository<Filter, int> Filters { get; }
        IBaseRepository<Lot, int> Lots { get; }
        Task SaveChanges();
        void RollBack();
    }
}
