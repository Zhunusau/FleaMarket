using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.DAL.Models.Mappings
{
    public class UserProfileMap : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
