﻿using Autofac;
using GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper;
using GodelMastery.FleaMarket.BL.Core.Helpers.SchedulerHelper;
using GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper;
using GodelMastery.FleaMarket.BL.Interfaces;
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
                .InstancePerLifetimeScope();

            builder
                .RegisterType<FilterService>()
                .As<IFilterService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<ConfigProvider>()
                .As<IConfigProvider>()
                .InstancePerLifetimeScope();

            builder.RegisterType<HtmlParserProvider>()
                .As<IHtmlParserProvider>()
                .SingleInstance();
            builder
                .RegisterType<EmailProvider>()
                .As<IEmailProvider>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<SchedulerManager>()
                .As<ISchedulerManager>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}