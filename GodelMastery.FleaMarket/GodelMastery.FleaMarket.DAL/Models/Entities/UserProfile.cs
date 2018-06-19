using System.Collections.Generic;

namespace GodelMastery.FleaMarket.DAL.Models.Entities
{
    public class UserProfile : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Icon { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }

        public UserProfile()
        {
            Filters = new List<Filter>();
        }
    }
}
