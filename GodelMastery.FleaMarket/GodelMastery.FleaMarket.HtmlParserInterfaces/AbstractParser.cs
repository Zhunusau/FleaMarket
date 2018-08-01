using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;

namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public abstract class AbstractParser : IParser<List<HtmlLot>>
    {
        protected abstract bool GetError(IHtmlDocument document);
        protected abstract IEnumerable<IElement> GetLots(IHtmlDocument document);
        protected abstract string GetSourceId(IElement lot);
        protected abstract string GetName(IElement lot);
        protected abstract string GetPrice(IElement lot);
        protected abstract string GetLocation(IElement lot);
        protected abstract string GetLink(IElement lot);
        protected abstract string GetImage(IElement lot);
        protected abstract string GetDate(IElement lot);

        public List<HtmlLot> Parse(IHtmlDocument document)
        {
            var htmlLots = new List<HtmlLot>();

            if (GetError(document))
            {
                return null;
            }

            var lots = GetLots(document);

            foreach (var lot in lots)
            {
                htmlLots.Add(new HtmlLot
                {
                    SourceId = GetSourceId(lot),
                    Name = GetName(lot),
                    Price = GetPrice(lot),
                    Location = GetLocation(lot),
                    Link = GetLink(lot),
                    Image = GetImage(lot),
                    DateOfUpdate = GetDate(lot)
                });
            }
            return htmlLots;
        }
    }
}
