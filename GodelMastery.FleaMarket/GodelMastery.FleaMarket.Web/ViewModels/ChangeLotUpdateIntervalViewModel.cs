using System.Collections.Generic;
using System.Web.Mvc;

namespace GodelMastery.FleaMarket.Web.ViewModels
{
    public class ChangeLotUpdateIntervalViewModel
    {
        public string SelectedValue { get; set; }
        public IEnumerable<SelectListItem> UpdateLotIntervals { get; set; }
    }
}