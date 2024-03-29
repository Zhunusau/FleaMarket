﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Text.RegularExpressions;
using NLog;
using GodelMastery.FleaMarket.Web.App_Start;

namespace GodelMastery.FleaMarket.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var error = Server.GetLastError();
            logger.Error(error,$"Unhandled error Message: {error.Message} InnerException: {error.InnerException}");

            Regex pattern = new Regex("[\t\r\n:]");
            var val = pattern.Replace(error.Message, " ");

            Response.Redirect($"~/Error/HandleExceptions/?{val}");

            Server.ClearError();
        }
    }
}
