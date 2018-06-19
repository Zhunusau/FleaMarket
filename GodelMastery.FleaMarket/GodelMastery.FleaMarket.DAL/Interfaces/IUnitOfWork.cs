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
        IBaseRepository<Filter> Filters { get; }
        IBaseRepository<Lot> Lots { get; }
        IBaseRepository<UserProfile> UserProfiles { get; }
        Task SaveChanges();
        void RollBack();
    }
}
