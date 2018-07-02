using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Dtos;
using System.Collections.Generic;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces
{
    public interface IDashboardModelFactory
    {
        DashboardModel CreateDashboardModel(IEnumerable<FilterDto> filterDto, UserDto userDto);
    }
}
