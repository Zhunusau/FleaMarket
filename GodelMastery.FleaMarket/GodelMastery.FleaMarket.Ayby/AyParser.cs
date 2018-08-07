using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.Ayby
{
    public class AyParser : AbstractParser
    {

        protected override bool GetError(IHtmlDocument document)
        {
            return false;
        }

        protected override IEnumerable<IElement> GetLots(IHtmlDocument document)
        {
            var tableLots = document.QuerySelectorAll("li")
                .Where(item => item.ClassName != null && item.ClassName.Equals("viewer-type-grid__li "));
            return tableLots;
        }

        protected override string GetSourceId(IElement lot)
        {
            var sourceId = lot.GetAttribute("data-value");
            return sourceId;
        }

        protected override string GetName(IElement lot)
        {
            var name = lot.QuerySelectorAll("p")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("item-type-card__title"))?
                .QuerySelector("span")
                .TextContent;
            return name;
        }

        protected override string GetImage(IElement lot)
        {
            var image = lot.QuerySelector("img").GetAttribute("src");
            return image;
        }

        protected override string GetLink(IElement lot)
        {
            var link = lot.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("item-type-card__link"))?
                .GetAttribute("href");
            return link;
        }

        protected override string GetPrice(IElement lot)
        {
            var price = lot.QuerySelectorAll("span")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("c-hot"))?
                .QuerySelector("strong")
                .TextContent
                .Remove(3, 3);
            return price;
        }

        protected override string GetDate(IElement lot)
        {
            var date = lot.QuerySelectorAll("small")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("item-type-card__time"))?
                .TextContent;
            return date;
        }

        protected override string GetLocation(IElement lot)
        {
            var name = lot.QuerySelectorAll("p")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("item-type-card__title"))?
                .TextContent;
            return name;
        }
    }
}
