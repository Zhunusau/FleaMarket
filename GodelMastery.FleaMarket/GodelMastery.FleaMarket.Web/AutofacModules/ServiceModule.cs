using Autofac;
using GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper;
using GodelMastery.FleaMarket.BL.Services;

namespace GodelMastery.FleaMarket.Web.AutofacModules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(BaseService).Assembly)
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder
                .RegisterType<EmailProvider>()
                .As<IEmailProvider>()
                .SingleInstance();

            builder
                .RegisterType<ConfigProvider>()
                .As<IConfigProvider>()
                .SingleInstance();

            builder.RegisterType<HtmlParserProvider>()
                .As<IHtmlParserProvider>()
                .SingleInstance();

            base.Load(builder);
        }
    }
}