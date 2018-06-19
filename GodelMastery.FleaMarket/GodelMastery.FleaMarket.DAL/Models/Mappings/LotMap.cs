using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.DAL.Models.Mappings
{
    public class LotMap : EntityTypeConfiguration<Lot>
    {
        public LotMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(x => x.Location)
                .IsRequired()
                .HasMaxLength(255);

            Property(x => x.Link)
                .IsRequired()
                .HasMaxLength(512);

            Property(x => x.Price)
                .IsRequired()
                .HasPrecision(19, 2);

            Property(x => x.DateOfFound)
                .IsRequired();

            Property(x => x.Status)
                .IsRequired();
        }
    }
}
