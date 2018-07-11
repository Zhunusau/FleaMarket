using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;


namespace GodelMastery.FleaMarket.Web.App_Start
{
    public static class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            return container;
        }
    }
}