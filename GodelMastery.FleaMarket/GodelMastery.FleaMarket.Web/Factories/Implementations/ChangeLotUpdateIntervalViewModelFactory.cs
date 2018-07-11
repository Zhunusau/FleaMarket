using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.Helpers;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Factories.Implementations
{
    public class ChangeLotUpdateIntervalViewModelFactory : IChangeLotUpdateIntervalViewModelFactory
    {
        private readonly IConfigProvider configProvider;
        private readonly LotUpdateIntervalConfigModel lotUpdateIntervalConfigModel;

        public ChangeLotUpdateIntervalViewModelFactory(IConfigProvider configProvider)
        {
            this.configProvider = configProvider ?? throw new ArgumentNullException(nameof(configProvider));
            this.lotUpdateIntervalConfigModel = configProvider.ConfigurateLotUpdateIntervalConfigModel() ?? throw new ArgumentNullException(nameof(lotUpdateIntervalConfigModel));
        }

        public ChangeLotUpdateIntervalViewModel CreateChangeUpdateLotIntervalViewModel()
        {
            return new ChangeLotUpdateIntervalViewModel
            {
                UpdateLotIntervals = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = DateTimeHelper.GetInterval(lotUpdateIntervalConfigModel.EveryTenMinutes),
                        Value = lotUpdateIntervalConfigModel.EveryTenMinutes.ToString()
                    },
                    new SelectListItem
                    {
                        Text = DateTimeHelper.GetInterval(lotUpdateIntervalConfigModel.EveryHour),
                        Value = lotUpdateIntervalConfigModel.EveryHour.ToString()
                    },
                    new SelectListItem
                    {
                        Text = DateTimeHelper.GetInterval(lotUpdateIntervalConfigModel.EveryThreeHours),
                        Value = lotUpdateIntervalConfigModel.EveryThreeHours.ToString()
                    },
                    new SelectListItem
                    {
                        Text = DateTimeHelper.GetInterval(lotUpdateIntervalConfigModel.EveryTwelveHours),
                        Value = lotUpdateIntervalConfigModel.EveryTwelveHours.ToString()
                    },
                    new SelectListItem
                    {
                        Text = DateTimeHelper.GetInterval(lotUpdateIntervalConfigModel.EveryDay),
                        Value = lotUpdateIntervalConfigModel.EveryDay.ToString()
                    },
                    new SelectListItem
                    {
                        Text = "None",
                        Value = "None"
                    },
                }
            };
        }
    }
}