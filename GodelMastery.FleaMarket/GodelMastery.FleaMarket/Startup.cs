using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GodelMastery.FleaMarket.Startup))]
namespace GodelMastery.FleaMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
