using System;
using System.Data.Entity;
using System.IO;
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
            var filter = new Filter {FilterName = "Iphone", Content = "Iphone 6s 32 gb silver"};
            user.Filters.Add(filter);
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "Must be deleted",
                Location = "Минск, Фрунзенский",
                SourceId = 98
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "Must be deleted",
                Location = "Минск, Фрунзенский",
                SourceId = 99
            });
            filter.Lots.Add(new Lot
            {
                Price = 199.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "Price must be updated",
                Location = "Минск, Фрунзенский",
                SourceId = 3
            });
            filter.Lots.Add(new Lot
            {
                Price = 199.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "Price must be updated",
                Location = "Минск, Фрунзенский",
                SourceId = 4
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "Price must be updated",
                Location = "Минск, Фрунзенский",
                SourceId = 5
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "Price must be updated",
                Location = "Минск, Фрунзенский",
                SourceId = 6
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "No changes",
                Location = "Минск, Фрунзенский",
                SourceId = 7
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "No changes",
                Location = "Минск, Фрунзенский",
                SourceId = 8
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "No changes",
                Location = "Минск, Фрунзенский",
                SourceId = 9
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "No changes",
                Location = "Минск, Фрунзенский",
                SourceId = 10
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "No changes",
                Location = "Минск, Фрунзенский",
                SourceId = 11
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "No changes",
                Location = "Минск, Фрунзенский",
                SourceId = 12
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "No changes",
                Location = "Минск, Фрунзенский",
                SourceId = 13
            });
            filter.Lots.Add(new Lot
            {
                Price = 200.00m,
                Link = "https://www.kufar.by/%D0%A4%D1%80%D1%83%D0%BD%D0%B7%D0%B5%D0%BD%D1%81%D0%BA%D0%B8%D0%B9/%D0%A2%D0%B5%D0%BB%D0%B5%D1%84%D0%BE%D0%BD%D1%8B/IPhone_6s_Space_Gray_16_10_10_48833322.htm",
                DateOfFound = DateTime.Now,
                DateOfUpdate = DateTime.Now,
                FilterId = filter.Id,
                Image = "https://cache5.kufar.by/line_thumbs_small/16/1630712080.jpg",
                Name = "No changes",
                Location = "Минск, Фрунзенский",
                SourceId = 14
            });
            var filter1 = new Filter { FilterName = "Bmw", Content = "Bmw e46 black color" };
            user.Filters.Add(filter1);
            var filter2 = new Filter { FilterName = "Lenovo", Content = "Lenovo y50-70" };
            user.Filters.Add(filter2);
            var filter3 = new Filter { FilterName = "Samsung", Content = "Samsung galaxy s9" };
            user.Filters.Add(filter3);
            var filter4 = new Filter { FilterName = "Xiomi", Content = "Xiomi s90" };
            user.Filters.Add(filter4);
            
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
