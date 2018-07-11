using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace GodelMastery.FleaMarket.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult HandleExceptions()
        {
            var errorMessage = Request.QueryString.ToString();

            Regex pattern = new Regex("[+%]");
            errorMessage = pattern.Replace(errorMessage, " ");
            ViewBag.Message = !String.IsNullOrEmpty(errorMessage) ? errorMessage : "Unknown exception occurs";
            return View();
        }
    }
}