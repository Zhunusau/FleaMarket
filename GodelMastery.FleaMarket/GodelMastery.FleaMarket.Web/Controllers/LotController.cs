using System;
using System.Web.Mvc;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    [Authorize]
    public class LotController : Controller
    {
        private readonly ILotService lotService;
        private readonly IFilterService filterService;
        private readonly IFilterViewModelFactory filterViewModelFactory;
        private readonly ILotViewModelFactory lotViewModelFactory;

        public LotController(
            ILotService lotService,
            IFilterService filterService,
            IFilterViewModelFactory filterViewModelFactory,
            ILotViewModelFactory lotViewModelFactory)
        {
            this.lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
            this.filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
            this.filterViewModelFactory = filterViewModelFactory ?? throw new ArgumentNullException(nameof(filterViewModelFactory));
            this.lotViewModelFactory = lotViewModelFactory ?? throw new ArgumentNullException(nameof(lotViewModelFactory));
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
    }
}