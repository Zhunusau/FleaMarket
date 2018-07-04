using System.Threading.Tasks;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.HtmlParser
{
    public class HtmlLoader : IHtmlLoader
    {
        private IParserSettings parserSettings;

        public HtmlLoader(IParserSettings parserSettings)
        {
            this.parserSettings = parserSettings;
        }

        public Task<string> GetSourceByPageId(int page)
        {
            throw new System.NotImplementedException();
        }
    }
}
