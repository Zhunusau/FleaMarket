using System.Web.Mvc;

namespace GodelMastery.FleaMarket.Web.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper html, string img)
        {
            return new MvcHtmlString("<img class=\"img-rounded\" src='" + img + "' height=\"100\" alt=\"\">");
        }

        public static MvcHtmlString Price(this HtmlHelper html, decimal price)
        {
            return new MvcHtmlString(price.ToString(true));
        }
    }
}