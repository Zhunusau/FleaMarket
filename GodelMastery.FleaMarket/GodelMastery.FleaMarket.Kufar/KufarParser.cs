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

        protected override string GetSourceId(IElement lot)
        {
            var imageElement = GetImageElement(lot);
            var sourceId = imageElement?.Attributes["name"].Value;
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
                .FirstOrDefault(item => item.Attributes != null && item.Attributes["dir"].Value.Contains("ltr"))?
                .TextContent.Trim(charTrim).Replace(" ", "");
            return price;
        }

        protected override string GetLocation(IElement lot)
        {
            var location = lot?.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__location"))?
                .TextContent.Trim();
            return location;
        }

        protected override string GetLink(IElement lot)
        {
            var link = lot?.Attributes["href"].Value;
            return link;
        }

        protected override string GetImage(IElement lot)
        {
            string image = null;
            var imageElement = GetImageElement(lot);
            var dataImages = imageElement?.Attributes["data-images"];
            if (dataImages != null && !dataImages.Value.Equals(""))
            {
                var imageLink = dataImages.Value.Split(',');
                image = string.Concat("https://content.kufar.by/line_thumbs", imageLink[0]);
            }
            return image;
        }

        protected override string GetDate(IElement lot)
        {
            var time = lot.QuerySelectorAll("time")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__date"));
            var date = time?.Attributes["datetime"].Value;
            return date;
        }

        private static IElement GetImageElement(IElement lot)
        {
            var imageElement = lot.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__image"));
            return imageElement;
        }

        //public List<KufarHtmlLot> Parse(IHtmlDocument document)
        //{
        //    var lots = new List<KufarHtmlLot>();

        //    if (GetErrorPage(document) != null)
        //    {
        //        return null;
        //    }

        //    var articles = GetArticles(document);

        //    foreach (var article in articles)
        //    {
        //        var imageElement = GetImageElement(article);
        //        var infoContainer = GetInfoContainer(article);

        //        lots.Add(new KufarHtmlLot
        //        {
        //            SourceId = GetSourceId(imageElement),
        //            Name = GetName(infoContainer),
        //            Price = GetPrice(infoContainer),
        //            Location = GetLocation(infoContainer),
        //            Link = GetLink(imageElement),
        //            Image = GetImage(imageElement),
        //            DateOfUpdate = GetDateOfUpdate(infoContainer)
        //        });
        //    }
        //    return lots;
        //}

        //protected override bool GetErrorPage(IParentNode document) //Looking for a page with "list_error"
        //{
        //    var error = document.QuerySelectorAll("div")
        //        .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_error"));
        //    return error == null;
        //}

        //private static IEnumerable<IElement> GetArticles(IParentNode document)
        //{
        //    var articles = document.QuerySelectorAll("article")
        //        .Where(item => item.ClassName != null && item.ClassName.Equals("list_ads__item "));
        //    return articles;
        //}

        //private static string GetImage(IElement imageElement)
        //{
        //    string image = null;
        //    var dataImages = imageElement?.Attributes["data-images"];
        //    if (dataImages != null && !dataImages.Value.Equals(""))
        //    {
        //        var imageLink = dataImages.Value.Split(',');
        //        image = string.Concat("https://content.kufar.by/line_thumbs", imageLink[0]);
        //    }
        //    return image;
        //}

        //private static IElement GetInfoContainer(IElement article)
        //{
        //    var infoContainer = article.QuerySelectorAll("div")
        //        .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__info_container"));
        //    return infoContainer;
        //}

        //private static string GetName(IElement infoContainer)
        //{
        //    var name = infoContainer?.QuerySelectorAll("a")
        //        .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__title"));
        //    name?.Children
        //        .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list__ribbons"))?
        //        .Remove(); //delete <span> "скидка" "акция" "бомба" "хит продаж"
        //    return name?.TextContent.Trim();
        //}

        //private static string GetLocation(IElement infoContainer)
        //{
        //    var location = infoContainer?.QuerySelectorAll("a")
        //        .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__location"))?
        //        .TextContent.Trim();
        //    return location;
        //}

        //private static string GetPrice(IElement infoContainer)
        //{
        //    char[] charTrim = { ' ', 'р', '.', '\n' };
        //    var price = infoContainer?.QuerySelectorAll("b")
        //        .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__price"))?
        //        .QuerySelectorAll("span").FirstOrDefault()?
        //        .TextContent.Trim(charTrim).Replace(" ", "");
        //    return price;
        //}

        //private static string GetLink(IElement imageElement)
        //{
        //    var link = imageElement?.Attributes["href"].Value;
        //    return link;
        //}

        //private static string GetSourceId(IElement imageElement)
        //{
        //    var sourceId = imageElement?.Attributes["name"].Value;
        //    return sourceId;
        //}

    }
}
