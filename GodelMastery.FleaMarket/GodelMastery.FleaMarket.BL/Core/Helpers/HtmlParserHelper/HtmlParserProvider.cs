using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.Ayby;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Kufar;
using GodelMastery.FleaMarket.HtmlParser;
using GodelMastery.FleaMarket.HtmlParserInterfaces;
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
                var parser1 = new HtmlParser<List<HtmlLot>>(new KufarParser(), new KufarSettings(filterContent));
                var parser2 = new HtmlParser<List<HtmlLot>>(new AyParser(), new AySettings(filterContent));
                var htmlPages1 = await parser1.GetHtmlPages();
                var htmlPages2 = await parser2.GetHtmlPages();
                var lots1 = htmlPages1.SelectMany(x => x.Select(
                    lot => new LotDto
                    {
                        Name = lot.Name,
                        Link = lot.Link,
                        Image = lot.Image,
                        Location = lot.Location,
                        SourceId = lot.SourceId,
                        Price = Convert.ToDecimal(lot.Price),
                        DateOfUpdate = Convert.ToDateTime(lot.DateOfUpdate)
                    })).ToList();
                var lots2 = htmlPages2.SelectMany(x => x.Select(
                    lot => new LotDto
                    {
                        Name = lot.Name,
                        Link = lot.Link,
                        Image = lot.Image,
                        Location = lot.Location,
                        SourceId = lot.SourceId,
                        Price = Convert.ToDecimal(lot.Price),
                        DateOfUpdate = Convert.ToDateTime(lot.DateOfUpdate)
                    })).ToList();
                lots1.AddRange(lots2);
                return lots1;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
