using System.Web.Mvc;

namespace GodelMastery.FleaMarket.Web.Helpers
{
    public static class HtmExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper html, string img)
        {
            return new MvcHtmlString("<img class=\"img-rounded\" src='" + img + "' height=\"100\" alt=\"\">");
        }
    }
}