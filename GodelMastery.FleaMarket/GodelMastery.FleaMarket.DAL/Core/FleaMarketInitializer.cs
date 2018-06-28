using System;
using System.Data.Entity;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GodelMastery.FleaMarket.DAL.Core
{
    public class FleaMarketInitializer : DropCreateDatabaseIfModelChanges<FleaMarketContext>
    {
        protected override void Seed(FleaMarketContext context)
        {
            var role = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            role.Create(new ApplicationRole { Name = "Administrator" });
            role.Create(new ApplicationRole { Name = "User" });
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var adminUser = new ApplicationUser
            {
                FirstName = "Vlad",
                LastName = "Kuzmich",
                Email = "admin@test.com",
                UserName = "admin@test.com"
            };
            userManager.Create(adminUser, "123321");
            var admin = userManager.FindByEmail(adminUser.Email);
            userManager.AddToRole(admin.Id, "Administrator");
            var commonUser = new ApplicationUser
            {
                FirstName = "Vlad",
                LastName = "Kuzmich",
                Email = "user@test.com",
                UserName = "user@test.com"
            };
            userManager.Create(commonUser, "12345678");
            var user = userManager.FindByEmail(commonUser.Email);
            userManager.AddToRole(user.Id, "User");
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
