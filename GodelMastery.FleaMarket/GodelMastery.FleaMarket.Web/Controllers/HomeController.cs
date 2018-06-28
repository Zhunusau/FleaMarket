using System.Web.Mvc;
using NLog;

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