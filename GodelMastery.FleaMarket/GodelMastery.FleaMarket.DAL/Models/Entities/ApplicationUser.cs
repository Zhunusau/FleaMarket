using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
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