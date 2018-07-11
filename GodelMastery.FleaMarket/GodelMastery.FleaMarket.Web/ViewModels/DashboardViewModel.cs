using System.Collections.Generic;

namespace GodelMastery.FleaMarket.Web.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<FilterViewModel> FilterViewModels { get; set; }
        public UserInfoViewModel UserInfoViewModel { get; set; }
        public string ErrorMessage { get; set; }
    }
}