using Autofac;
using GodelMastery.FleaMarket.BL.Core.ModelFactories;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.Web.Factories.Implementations;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;

namespace GodelMastery.FleaMarket.Web.AutofacModules
{
    public class FactoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<FilterModelFactory>()
                .As<IFilterModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<UserDtoModelFactory>()
                .As<IUserDtoModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<FilterViewModelFactory>()
                .As<IFilterViewModelFactory>()
                .SingleInstance();
        }
    }
}