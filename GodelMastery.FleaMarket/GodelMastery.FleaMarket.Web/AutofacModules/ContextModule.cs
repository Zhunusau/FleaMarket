using Autofac;
using GodelMastery.FleaMarket.DAL.Core;
using System.Data.Entity;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
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
                .InstancePerRequest();

            builder
                .RegisterType<UserManager<ApplicationUser>>()
                .WithProperty("UserTokenProvider",
                    new DataProtectorTokenProvider<ApplicationUser>(GetDpapiDataProtectionProvider().Create("IdentityProvider")))
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<RoleManager<ApplicationRole>>()
                .AsSelf()
                .InstancePerRequest();

            builder
                .RegisterType<UserStore<ApplicationUser>>()
                .UsingConstructor(typeof(DbContext))
                .As<IUserStore<ApplicationUser>>()
                .InstancePerRequest();

            builder
                .RegisterType<RoleStore<ApplicationRole>>()
                .UsingConstructor(typeof(DbContext))
                .As<IRoleStore<ApplicationRole, string>>()
                .InstancePerRequest();

            builder
                .Register(c => HttpContext.Current.GetOwinContext().Authentication)
                .As<IAuthenticationManager>()
                .InstancePerRequest();
        }

        #region Configurations
        private DpapiDataProtectionProvider GetDpapiDataProtectionProvider()
        {
            return new DpapiDataProtectionProvider("GodelMastery.FleaMarket.Provider");
        }
        #endregion
    }
}