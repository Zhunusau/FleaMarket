using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using NLog;

namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public abstract class AbstractParser : IParser<List<HtmlLot>>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

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
            try
            {
                if (GetError(document))
                {
                    return null;
                }
                var lots = GetLots(document);
                return lots.Select(lot => new HtmlLot
                {
                    SourceId = GetSourceId(lot),
                    Name = GetName(lot),
                    Price = GetPrice(lot),
                    Location = GetLocation(lot),
                    Link = GetLink(lot),
                    Image = GetImage(lot),
                    DateOfUpdate = GetDate(lot)
                }).ToList();
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
