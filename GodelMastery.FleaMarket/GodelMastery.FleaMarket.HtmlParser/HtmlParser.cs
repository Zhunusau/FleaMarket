using System;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.HtmlParser
{
    public class HtmlParser<T> : IHtmlParser<T> where T : class
    {
        private IHtmlLoader htmlLoader;

        public HtmlParser(IHtmlLoader htmlLoader)
        {
            this.htmlLoader = htmlLoader;
        }

        public bool IsActive => throw new NotImplementedException();
        public IParser<T> Parser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IParserSettings Settings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
