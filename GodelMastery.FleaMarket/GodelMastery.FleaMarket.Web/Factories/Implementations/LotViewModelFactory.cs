using System;
using System.Collections.Generic;
using System.Linq;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.ViewModels;
using PagedList;

namespace GodelMastery.FleaMarket.Web.Factories.Implementations
{
    public class LotViewModelFactory : ILotViewModelFactory
    {
        public LotViewModel CreateLotViewModel(LotDto lotDto)
        {
            return new LotViewModel
            {
                Name = lotDto.Name,
                Price = lotDto.Price,
                Location = lotDto.Location,
                Image = lotDto.Image,
                DateOfUpdate = lotDto.DateOfUpdate,
                FilterId = lotDto.FilterId,
                Link = lotDto.Link
            };
        }

        public IEnumerable<LotViewModel> CreateLotViewModels(IEnumerable<LotDto> lotDtos)
        {
            return lotDtos.Select(x => CreateLotViewModel(x));
        }

        public MonitoringPageViewModel CreateMonitoringPageViewModel(IEnumerable<LotViewModel> lots, FilterViewModel filterViewModel, int? page)
        {
            return new MonitoringPageViewModel
            {
                FilterViewModel = filterViewModel,
                LotViewModels = lots.ToPagedList(page ?? 1, 10)
            };
        }
    }
}