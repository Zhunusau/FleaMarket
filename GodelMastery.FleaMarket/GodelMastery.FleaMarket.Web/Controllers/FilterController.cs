using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.Helpers;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    [Authorize]
    public class FilterController : Controller
    {
        private readonly IDashboardService dashboardService;
        private readonly IFilterViewModelFactory filterViewModelFactory;
        private readonly IUserService userService;
        private readonly IFilterService filterService;

        public FilterController(
            IFilterService filterService,
            IDashboardService dashboardService,
            IFilterViewModelFactory filterViewModelFactory,
            IUserService userService)
        {
            this.filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
            this.dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));
            this.filterViewModelFactory = filterViewModelFactory ?? throw new ArgumentNullException(nameof(filterViewModelFactory));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        public async Task<ActionResult> Dashboard()
        {
            var currentLogin = CurrentUser.GetUserName();
            var dashboardModel = await dashboardService.GetContent(currentLogin);
            var isAccountVerified = await userService.IsAccountVerified(currentLogin);
            if (!isAccountVerified)
            {
                return View(filterViewModelFactory.CreateDashboardViewModel(dashboardModel, ErrorMessagesHelper.AccountNotVerifiedMessage));
            }
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

        [HttpGet]
        public ActionResult RemoveFilter(int filterId)
        {
            var filter = filterService.GetFilterById(filterId);
            if (filter != null )
            {
                var filterViewModel = filterViewModelFactory.CreateFilterViewModel(filter);
                return PartialView(filterViewModel);
            }
            return View("Dashboard");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("RemoveFilter")]
        public async Task<ActionResult> RemoveFilterConfirm(int filterId)
        {
            await filterService.RemoveFilter(filterId);
            return RedirectToAction("Dashboard");
        }

    }
}