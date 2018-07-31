using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.Ayby
{
    public class AyParser : IParser<List<AyHtmlLot>>
    {
        public List<AyHtmlLot> Parse(IHtmlDocument document)
        {
            var lots = new List<AyHtmlLot>();

            var tableLots = GetTableLots(document);
            foreach (var lot in tableLots)
            {
                var sourceId = lot.GetAttribute("data-value");
                var name = lot.QuerySelectorAll("span")
                    .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("g-highlight"))?
                    .TextContent;
                var imageContainer = lot.QuerySelectorAll("span")
                    .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("item-type-card__figure"));
                var image = imageContainer?.QuerySelector("img").Attributes["src"].Value;
                var link = lot.QuerySelectorAll("a")
                    .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("item-type-card__link"))?
                    .Attributes["href"]
                    .Value;

                lots.Add(new AyHtmlLot
                {
                    SourceId = sourceId,
                    Name = name,
                    Price = "100",
                    Location = "Test",
                    Link = link,
                    Image = image,
                    DateOfUpdate = "2018-07-30T15:14:45"
                });
            }
            return lots;
        }

        private static IEnumerable<IElement> GetTableLots(IParentNode document)
        {
            var tableLots = document.QuerySelectorAll("li")
                .Where(item => item.ClassName != null && item.ClassName.Equals("viewer-type-grid__li "));
            return tableLots;
        }

        //private static string GetSourceId(IElement lot)
        //{
        //    var sourceId = lot?.GetAttribute("data-value");
        //    return sourceId;
        //}

        //private static string GetName(IParentNode lot)
        //{
        //    var nameContainer = lot.QuerySelector("p .item-type-card__title").QuerySelector("span .g-highlight").TextContent;
        //    return nameContainer;
        //}

        //private static string GetImage(IParentNode lot)
        //{
        //    var imageContainer = lot.QuerySelectorAll("span")
        //        .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("item-type-card__figure"));
        //    var image = imageContainer?.QuerySelector("img").Attributes["src"].Value;
        //    return image;
        //}

        //private static string GetLink(IElement lot)
        //{
        //    var link = lot.QuerySelectorAll("a")
        //        .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("item-type-card__link"))?
        //        .Attributes["href"]
        //        .Value;
        //    return link;
        //}
    }
}
