using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Dtos;

namespace GodelMastery.FleaMarket.BL.BusinessModels
{
    public class DashboardModel
    {
        public IEnumerable<FilterDto> FilterDtos { get; set; }
        public UserDto UserDto { get; set; }
    }
}
