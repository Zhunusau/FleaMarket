using Autofac;
using GodelMastery.FleaMarket.DAL.Core;
using System.Data.Entity;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GodelMastery.FleaMarket.Web.AutofacModules
{
    public class ContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<FleaMarketContext>()
                .As<DbContext>()
                .InstancePerRequest();

            builder
                .RegisterType<UserManager<ApplicationUser>>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<RoleManager<ApplicationRole>>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<RoleStore<ApplicationRole>>()
                .UsingConstructor(typeof(DbContext))
                .As<IRoleStore<ApplicationRole, string>>()
                .InstancePerRequest();

            builder
                .RegisterType<UserStore<ApplicationUser>>()
                .UsingConstructor(typeof(DbContext))
                .As<IUserStore<ApplicationUser>>()
                .InstancePerRequest();
        }
    }
}