using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.Kufar
{
    public class KufarParser : AbstractParser
    {
        protected override bool GetError(IHtmlDocument document)
        {
            var error = document.QuerySelectorAll("div")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_error"));
            return error != null;
        }

        protected override IEnumerable<IElement> GetLots(IHtmlDocument document)
        {
            var lots = document.QuerySelectorAll("article")
                .Where(item => item.ClassName != null && item.ClassName.Equals("list_ads__item "));
            return lots;
        }

        private static IElement GetImageElement(IElement lot)
        {
            var imageElement = lot.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__image"));
            return imageElement;
        }

        protected override string GetSourceId(IElement lot)
        {
            var imageElement = GetImageElement(lot);
            var sourceId = imageElement.GetAttribute("name");
            return sourceId;
        }

        protected override string GetName(IElement lot)
        {
            var name = lot.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__title"));
            name?.Children
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list__ribbons"))?
                .Remove(); //delete <span> "скидка" "акция" "бомба" "хит продаж"
            return name?.TextContent.Trim();
        }

        protected override string GetPrice(IElement lot)
        {
            char[] charTrim = { ' ', 'р', '.', '\n' };
            var price = lot?.QuerySelectorAll("span")
                .FirstOrDefault(item => item.GetAttribute("dir") != null && item.GetAttribute("dir").Contains("ltr"))?
                .TextContent;
            var replace = price?.Trim(charTrim).Replace(" ", "");
            return replace;
        }

        protected override string GetLocation(IElement lot)
        {
            var location = lot.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__location"))?
                .TextContent.Trim();
            return location;
        }

        protected override string GetLink(IElement lot)
        {
            var imageElement = GetImageElement(lot);
            var link = imageElement.GetAttribute("href");
            return link;
        }

        protected override string GetImage(IElement lot)
        {
            string image = null;
            var imageElement = GetImageElement(lot);
            var dataImages = imageElement.GetAttribute("data-images");
            if (dataImages != null && !dataImages.Equals(""))
            {
                var imageLink = dataImages.Split(',');
                image = string.Concat("https://content.kufar.by/line_thumbs", imageLink[0]);
            }
            return image;
        }

        protected override string GetDate(IElement lot)
        {
            var date = lot.QuerySelectorAll("time")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__date"))?
                .TextContent;
            return date;
        }
    }
}
