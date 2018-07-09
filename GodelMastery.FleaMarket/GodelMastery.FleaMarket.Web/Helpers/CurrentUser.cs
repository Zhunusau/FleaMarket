using System.ComponentModel.DataAnnotations;
using System.Web;
using Microsoft.AspNet.Identity;
using NLog;

namespace GodelMastery.FleaMarket.Web.Helpers
{
    public static class CurrentUser
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetUserId()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                logger.Error("CurrentUserId is NULL or Empty");
                throw new ValidationException(nameof(userId));
            }

            return userId;
        }

        public static string GetUserName()
        {
            var userName = HttpContext.Current.User.Identity.GetUserName();
            if (string.IsNullOrEmpty(userName))
            {
                logger.Error("CurrentUserName is NULL or Empty");
                throw new ValidationException(nameof(userName));
            }

            return userName;
        }
    }
}