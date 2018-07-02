using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using System.Collections.Generic;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Implementations
{
    public class DashboardModelFactory : IDashboardModelFactory
    {
        public DashboardModel CreateDashboardModel(IEnumerable<FilterDto> filterDto, UserDto userDto)
        {
            return new DashboardModel
            {
                FilterDtos = filterDto,
                UserDto = userDto
            };
        }
    }
}
