using Autofac;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Implementations;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
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
                .RegisterType<DashboardModelFactory>()
                .As<IDashboardModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<FilterModelFactory>()
                .As<IFilterModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<UserViewModelFactory>()
                .As<IUserViewModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<UserModelFactory>()
                .As<IUserModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<FilterViewModelFactory>()
                .As<IFilterViewModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<ChangeLotUpdateIntervalViewModelFactory>()
                .As<IChangeLotUpdateIntervalViewModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<LotViewModelFactory>()
                .As<ILotViewModelFactory>()
                .SingleInstance();

            builder
                .RegisterType<LotModelFactory>()
                .As<ILotModelFactory>()
                .SingleInstance();
        }
    }
}