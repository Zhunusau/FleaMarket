using System.Data.Entity.ModelConfiguration;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.DAL.Models.Mappings
{
    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {
            HasOptional(x => x.User)
                .WithRequired(x => x.ApplicationUser);
        }
    }
}
