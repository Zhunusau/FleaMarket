using Autofac;
using GodelMastery.FleaMarket.BL.Interfaces;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(GodelMastery.FleaMarket.Web.App_Start.Startup))]
namespace GodelMastery.FleaMarket.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = AutofacConfig.ConfigureContainer();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/SignIn"),
            });
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();
            GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(container));
            GlobalConfiguration.Configuration.UseSqlServerStorage("FleaMarketSheduler");
            container.Resolve<ISchedulerService>().InitializeUserSchedulers();
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}