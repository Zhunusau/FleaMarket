using System;
using Autofac;
using Hangfire;

namespace GodelMastery.FleaMarket.Web.App_Start
{
    public class ContainerJobActivator : JobActivator
    {
        private readonly IContainer container;

        public ContainerJobActivator(IContainer container)
        {
            this.container = container;
        }

        public override object ActivateJob(Type type)
        {
            return container.Resolve(type);
        }
    }
}