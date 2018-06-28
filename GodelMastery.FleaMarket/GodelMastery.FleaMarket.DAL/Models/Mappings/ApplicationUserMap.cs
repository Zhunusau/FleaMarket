using System.Data.Entity.ModelConfiguration;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.DAL.Models.Mappings
{
    public class ApplicationUserMap : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserMap()
        {

            Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
