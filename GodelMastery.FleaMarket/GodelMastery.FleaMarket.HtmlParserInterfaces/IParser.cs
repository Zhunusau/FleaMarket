using AngleSharp.Dom.Html;

namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
