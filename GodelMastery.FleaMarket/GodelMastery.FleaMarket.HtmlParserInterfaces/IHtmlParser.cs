using System.Collections.Generic;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public interface IHtmlParser<T> where T : class
    {
        Task<IEnumerable<T>> GetHtmlPages();
    }
}
