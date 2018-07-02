using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Factories.Interfaces
{
    public interface IFilterViewModelFactory
    {
        DashboardViewModel CreateDashboardViewModel(DashboardModel dashboardModel);
        FilterViewModel CreateFilterViewModel(FilterDto filterDto);
    }
}
