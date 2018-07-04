using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom.Html;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.Kufar
{
    public class KufarParser : IParser<List<KufarHtmlLot>>
    {
        public Task<List<KufarHtmlLot>> Parse(IHtmlDocument document)
        {
            throw new System.NotImplementedException();
        }
    }
}
