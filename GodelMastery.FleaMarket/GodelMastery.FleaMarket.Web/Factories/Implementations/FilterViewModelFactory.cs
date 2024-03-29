﻿using System.Linq;
using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.Helpers;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Factories.Implementations
{
    public class FilterViewModelFactory : IFilterViewModelFactory
    {
        public DashboardViewModel CreateDashboardViewModel(DashboardModel dashboardModel, string errorMessage = null)
        {
            return new DashboardViewModel
            {
                FilterViewModels = dashboardModel.FilterDtos.Select(x => CreateFilterViewModel(x)),
                UserInfoViewModel = new UserInfoViewModel
                {
                    Email = dashboardModel.UserDto.Email,
                    FirstName = dashboardModel.UserDto.FirstName,
                    LastName = dashboardModel.UserDto.LastName,
                    Icon = dashboardModel.UserDto.Icon,
                    LotUpdateInterval = dashboardModel.UserDto.LotUpdateInterval == null ? "None" : DateTimeHelper.GetInterval(dashboardModel.UserDto.LotUpdateInterval.Value)
                },
                ErrorMessage = errorMessage
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

        public FilterDto CreateFilterDto(FilterViewModel filterViewModel)
        {
            var filterDto = new FilterDto
            {
                FilterName = filterViewModel.FilterName,
                Content = filterViewModel.Content,
                ApplicationUserId = CurrentUser.GetUserId()
            };
            return filterDto;
        }
    }
}