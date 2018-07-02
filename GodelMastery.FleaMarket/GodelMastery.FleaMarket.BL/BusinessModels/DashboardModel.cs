using GodelMastery.FleaMarket.BL.Dtos;
using System.Collections.Generic;

namespace GodelMastery.FleaMarket.BL.BusinessModels
{
    public class DashboardModel
    {
        public IEnumerable<FilterDto> FilterDtos { get; set; }
        public UserDto UserDto { get; set; }
    }
}
