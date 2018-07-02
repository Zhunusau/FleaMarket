using System.Threading.Tasks;
using System.Web.Mvc;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    public class FilterController : Controller
    {
        private readonly IDashboardService dashboardService;
        private readonly IFilterViewModelFactory fileFilterViewModelFactory;

        public FilterController(IDashboardService dashboardService, IFilterViewModelFactory fileFilterViewModelFactory)
        {
            this.dashboardService = dashboardService;
            this.fileFilterViewModelFactory = fileFilterViewModelFactory;
        }

        [HttpGet]
        public async Task<ActionResult> Dashboard()
        {
            var currentLogin = System.Web.HttpContext.Current.User.Identity.Name;
            var dashboardModel = await dashboardService.GetContent(currentLogin);
            return View(fileFilterViewModelFactory.CreateDashboardViewModel(dashboardModel));
        }
    }
}