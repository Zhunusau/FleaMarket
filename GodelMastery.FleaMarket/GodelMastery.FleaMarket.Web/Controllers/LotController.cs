using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.Helpers;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    public class LotController : Controller
    {
        private readonly IChangeLotUpdateIntervalViewModelFactory changeLotUpdateIntervalViewModelFactory;
        private readonly ISchedulerService schedulerService;
        private readonly ILotViewModelFactory lotViewModelFactory;
        private readonly ILotService lotService;
        private readonly IFilterService filterService;
        private readonly IFilterViewModelFactory filterViewModelFactory;

        public LotController(
            IChangeLotUpdateIntervalViewModelFactory changeLotUpdateIntervalViewModelFactory,
            ILotViewModelFactory lotViewModelFactory,
            ISchedulerService schedulerService,
            ILotService lotService,
            IFilterService filterService,
            IUserService userService,
            IFilterViewModelFactory filterViewModelFactory)
        {
            this.changeLotUpdateIntervalViewModelFactory = changeLotUpdateIntervalViewModelFactory ?? throw new ArgumentNullException(nameof(changeLotUpdateIntervalViewModelFactory));
            this.lotViewModelFactory = lotViewModelFactory ?? throw new ArgumentException(nameof(lotViewModelFactory));
            this.schedulerService = schedulerService ?? throw new ArgumentNullException(nameof(schedulerService));
            this.lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
            this.filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
            this.filterViewModelFactory = filterViewModelFactory ?? throw new ArgumentNullException(nameof(filterViewModelFactory));
        }

        [HttpGet]
        public ActionResult MonitoringPage(int filterId, int? page)
        {
            var lotDtos = lotService.GetLotDtos(filterId);
            var filterDto = filterService.GetFilterById(filterId);
            var monitoringPageViewModel = lotViewModelFactory.CreateMonitoringPageViewModel
                (lotViewModelFactory.CreateLotViewModels(lotDtos), filterViewModelFactory.CreateFilterViewModel(filterDto), page);
            return View(monitoringPageViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateLots(int filterId)
        {
            await lotService.UpdateLots(filterId);
            return RedirectToAction("MonitoringPage", new { filterId = filterId });
        }

        [HttpGet]
        public ActionResult ChangeLotUpdateInterval()
        {
            return PartialView(changeLotUpdateIntervalViewModelFactory.CreateChangeUpdateLotIntervalViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> ChangeLotUpdateInterval(string selectedValue)
        {
            var currentLogin = CurrentUser.GetUserName();
            await schedulerService.ChangeLotUpdateInterval(currentLogin, selectedValue);
            return RedirectToAction("Dashboard", "Filter");
        }
    }
}