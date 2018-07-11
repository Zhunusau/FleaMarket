using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.Kufar
{
    public class KufarParser : IParser<List<KufarHtmlLot>>
    {
        public List<KufarHtmlLot> Parse(IHtmlDocument document)
        {
            var lots = new List<KufarHtmlLot>();

            if (GetErrorPage(document) != null)
            {
                return null;
            }

            foreach (var article in GetArticles(document))
            {
                var imageElement = GetImageElement(article);
                var infoContainer = GetInfoContainer(article);

                lots.Add(new KufarHtmlLot
                {
                    SourceId = GetSourceId(imageElement),
                    Name = GetName(infoContainer),
                    Price = GetPrice(infoContainer),
                    Location = GetLocation(infoContainer),
                    Link = GetLink(imageElement),
                    Image = GetImage(imageElement),
                    DateOfUpdate = GetDateOfUpdate(infoContainer)
                });
            }
            return lots;
        }

        private static IElement GetErrorPage(IParentNode document) //Looking for a page with "list_error"
        {
            var error = document.QuerySelectorAll("div")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_error"));
            return error;
        }

        private static IEnumerable<IElement> GetArticles(IParentNode document)
        {
            var articles = document.QuerySelectorAll("article")
                .Where(item => item.ClassName != null && item.ClassName.Equals("list_ads__item "));
            return articles;
        }

        private static IElement GetImageElement(IParentNode article)
        {
            var imageContainer = article.QuerySelectorAll("div")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__image_container"));
            var imageElement = imageContainer?.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__image"));
            return imageElement;
        }

        private static string GetImage(IElement imageElement)
        {
            string image = null;
            var dataImages = imageElement?.Attributes["data-images"];
            if (dataImages != null && !dataImages.Value.Equals(""))
            {
                var imageLink = dataImages.Value.Split(',');
                image = string.Concat("https://content.kufar.by/line_thumbs", imageLink[0]);
            }
            return image;
        }

        private static IElement GetInfoContainer(IParentNode article)
        {
            var infoContainer = article.QuerySelectorAll("div")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__info_container"));
            return infoContainer;
        }

        private static string GetName(IParentNode infoContainer)
        {
            var name = infoContainer?.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__title"));
            name?.Children
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list__ribbons"))?
                .Remove(); //delete <span> "скидка" "акция" "бомба" "хит продаж"
            return name?.TextContent.Trim();
        }

        private static string GetDateOfUpdate(IParentNode infoContainer)
        {
            var time = infoContainer?.QuerySelectorAll("time")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__date"));
            var date = time?.Attributes["datetime"].Value;
            return date;
        }

        private static string GetLocation(IParentNode infoContainer)
        {
            var location = infoContainer?.QuerySelectorAll("a")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__location"))?
                .TextContent.Trim();
            return location;
        }

        private static string GetPrice(IParentNode infoContainer)
        {
            char[] charTrim = { ' ', 'р', '.', '\n' };
            var price = infoContainer?.QuerySelectorAll("b")
                .FirstOrDefault(item => item.ClassName != null && item.ClassName.Contains("list_ads__price"))?
                .QuerySelectorAll("span").FirstOrDefault()?
                .TextContent.Trim(charTrim).Replace(" ", "");
            return price;
        }

        private static string GetLink(IElement imageElement)
        {
            var link = imageElement?.Attributes["href"].Value;
            return link;
        }

        private static string GetSourceId(IElement imageElement)
        {
            var sourceId = imageElement?.Attributes["name"].Value;
            return sourceId;
        }
    }
}
