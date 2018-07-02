using System.Linq;
using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Factories.Implementations
{
    public class FilterViewModelFactory : IFilterViewModelFactory
    {
        public DashboardViewModel CreateDashboardViewModel(DashboardModel dashboardModel)
        {
            return new DashboardViewModel
            {
                FilterViewModels = dashboardModel.FilterDtos.Select(x => CreateFilterViewModel(x)),
                UserInfoViewModel = new UserInfoViewModel
                {
                    Email = dashboardModel.UserDto.Email,
                    FirstName = dashboardModel.UserDto.FirstName,
                    LastName = dashboardModel.UserDto.LastName,
                    Icon = dashboardModel.UserDto.Icon
                }
            };
        }

        public FilterViewModel CreateFilterViewModel(FilterDto filterDto)
        {
            return new FilterViewModel
            {
                Id = filterDto.Id,
                FilterName = filterDto.FilterName,
                Content = filterDto.Content
            };
        }
    }
}