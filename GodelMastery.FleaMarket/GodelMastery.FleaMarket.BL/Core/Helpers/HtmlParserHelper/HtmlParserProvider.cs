using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Kufar;
using GodelMastery.FleaMarket.HtmlParser;
using NLog;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper
{
    public class HtmlParserProvider : IHtmlParserProvider
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<List<LotDto>> GetLotsByFilter(string filterContent)
        {
            try
            {
                logger.Info($"Started HtmlParserProvider with filter content \"{filterContent}\"");
                var parser = new HtmlParser<List<KufarHtmlLot>>(new KufarParser(), new KufarSettings(filterContent));
                var htmlPages = await parser.GetHtmlPages();
                var lots = htmlPages.SelectMany(x => x.Select(
                    lot => new LotDto
                    {
                        Name = lot.Name,
                        Link = lot.Link,
                        Image = lot.Image,
                        Location = lot.Location,
                        Price = Convert.ToDecimal(lot.Price),
                        SourceId = Convert.ToInt32(lot.SourceId),
                        DateOfUpdate = Convert.ToDateTime(lot.DateOfUpdate)
                    })).ToList();
                return lots;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
