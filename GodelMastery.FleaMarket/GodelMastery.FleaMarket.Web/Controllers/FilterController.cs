using System.Web.Mvc;
using GodelMastery.FleaMarket.BL.Interfaces;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    public class FilterController : Controller
    {
        private readonly IFilterService filterService;

        public FilterController(IFilterService filterService)
        {
            this.filterService = filterService;
        }
    }
}