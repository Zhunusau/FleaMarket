using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GodelMastery.FleaMarket.DAL.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Icon { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }

        public ApplicationUser()
        {
            Filters = new List<Filter>();
        }
    }
}