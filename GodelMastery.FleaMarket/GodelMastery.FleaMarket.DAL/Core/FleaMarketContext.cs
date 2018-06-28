using System.Data.Entity;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using GodelMastery.FleaMarket.DAL.Models.Mappings;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GodelMastery.FleaMarket.DAL.Core
{
    public class FleaMarketContext : IdentityDbContext<ApplicationUser>
    {
        public FleaMarketContext() :
            base("FleaMarket")
        {
            Database.SetInitializer(new FleaMarketInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new FilterMap());
            modelBuilder.Configurations.Add(new LotMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
