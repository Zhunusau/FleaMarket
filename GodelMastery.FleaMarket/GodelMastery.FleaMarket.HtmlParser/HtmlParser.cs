using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.HtmlParser
{
    public class HtmlParser<T> : IHtmlParser<T> where T : class
    {
        private readonly IParser<T> parser;
        private readonly IParserSettings settings;

        public HtmlParser(IParser<T> parser, IParserSettings settings)
        {
            this.parser = parser;
            this.settings = settings;
        }

        public async Task<IEnumerable<T>> GetHtmlPages()
        {
            using (var htmlLoader = new HtmlLoader(settings))
            {
                var htmlPages = new List<T>();
                //Can use while(true), however it will be very long
                for (var page = 1; page <= 3; page++)
                {
                    var source = await htmlLoader.GetSourceByPageId(page);
                    var domParser = new AngleSharp.Parser.Html.HtmlParser();
                    var document = await domParser.ParseAsync(source);
                    var result = parser.Parse(document);
                    if (result == null)
                    {
                        break;
                    }
                    htmlPages.Add(result);
                }
                return htmlPages;
            }
        }
    }
}
