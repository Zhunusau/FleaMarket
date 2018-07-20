using Autofac;
using GodelMastery.FleaMarket.DAL.Core;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Repositories;

namespace GodelMastery.FleaMarket.Web.AutofacModules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterGeneric(typeof(BaseRepository<,>))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}