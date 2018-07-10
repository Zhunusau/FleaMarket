using PagedList;

namespace GodelMastery.FleaMarket.Web.ViewModels
{
    public class MonitoringPageViewModel
    {
        public IPagedList<LotViewModel> LotViewModels { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}