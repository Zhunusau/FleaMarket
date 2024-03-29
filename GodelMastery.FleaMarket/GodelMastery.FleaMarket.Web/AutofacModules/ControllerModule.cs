﻿using Autofac;
using Autofac.Integration.Mvc;

namespace GodelMastery.FleaMarket.Web.AutofacModules
{
    public class ControllerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
        }
    }
}