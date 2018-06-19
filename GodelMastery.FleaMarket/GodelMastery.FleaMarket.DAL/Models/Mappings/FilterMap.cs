using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.DAL.Models.Mappings
{
    public class FilterMap : EntityTypeConfiguration<Filter>
    {
        public FilterMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.FilterName)
                .IsRequired()
                .HasMaxLength(255);

            Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(1024);
        }
    }
}
