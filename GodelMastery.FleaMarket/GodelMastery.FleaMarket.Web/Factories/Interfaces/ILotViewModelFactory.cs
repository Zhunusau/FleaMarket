using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Web.ViewModels;
using PagedList;

namespace GodelMastery.FleaMarket.Web.Factories.Interfaces
{
    public interface ILotViewModelFactory
    {
        LotViewModel CreateLotViewModel(LotDto lotDto);
        IEnumerable<LotViewModel> CreateLotViewModels(IEnumerable<LotDto> lotDtos);
        MonitoringPageViewModel CreateMonitoringPageViewModel(IEnumerable<LotViewModel> lots, FilterViewModel filterViewModel, int? page);
    }
}