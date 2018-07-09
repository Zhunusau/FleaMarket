using System.Web.Mvc;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.Helpers;
using GodelMastery.FleaMarket.Web.ViewModels;
using NLog;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    public class FilterController : Controller
    {
        private readonly IFilterService filterService;
        private readonly IDashboardService dashboardService;
        private readonly IFilterViewModelFactory filterViewModelFactory;

        public FilterController(IFilterService filterService, IDashboardService dashboardService, IFilterViewModelFactory fileFilterViewModelFactory)
        {
            this.filterService = filterService;
            this.dashboardService = dashboardService;
            this.filterViewModelFactory = fileFilterViewModelFactory;
        }

        [HttpGet]
        public async Task<ActionResult> Dashboard()
        {
            var currentLogin = CurrentUser.GetUserName();
            var dashboardModel = await dashboardService.GetContent(currentLogin);
            return View(filterViewModelFactory.CreateDashboardViewModel(dashboardModel));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FilterViewModel filterViewModel)
        {
            if (ModelState.IsValid)
            {
                var filterDto = filterViewModelFactory.CreateFilterDto(filterViewModel);
                var operationDetails = await filterService.Create(filterDto);
                if (operationDetails.Succeeded)
                {
                    return RedirectToAction("Dashboard", "Filter");
                }
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(filterViewModel);
        }
    }
}