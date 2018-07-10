using System.Web.Mvc;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}