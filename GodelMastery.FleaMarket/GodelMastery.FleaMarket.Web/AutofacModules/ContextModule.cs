using System.Data.Entity;
using Autofac;
using System.Web;
using GodelMastery.FleaMarket.DAL.Core;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

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
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UserManager<ApplicationUser>>()
                .WithProperty("UserTokenProvider", new DataProtectorTokenProvider<ApplicationUser>(GetDpapiDataProtectionProvider().Create("IdentityProvider")))
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RoleManager<ApplicationRole>>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UserStore<ApplicationUser>>()
                .UsingConstructor(typeof(DbContext))
                .As<IUserStore<ApplicationUser>>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<RoleStore<ApplicationRole>>()
                .UsingConstructor(typeof(DbContext))
                .As<IRoleStore<ApplicationRole, string>>()
                .InstancePerLifetimeScope();

            builder
                .Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .As<IAuthenticationManager>()
                .InstancePerLifetimeScope();
        }

        #region Configurations
        private static DpapiDataProtectionProvider GetDpapiDataProtectionProvider()
        {
            return new DpapiDataProtectionProvider("GodelMastery.FleaMarket.Provider");
        }
        #endregion
    }
}