using Microsoft.AspNet.Identity.EntityFramework;

namespace GodelMastery.FleaMarket.DAL.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual UserProfile User { get; set; }
    }
}
