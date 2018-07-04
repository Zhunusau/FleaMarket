using System.Threading.Tasks;
using AngleSharp.Dom.Html;

namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public interface IParser<T> where T : class
    {
        Task<T> Parse(IHtmlDocument document);
    }
}
