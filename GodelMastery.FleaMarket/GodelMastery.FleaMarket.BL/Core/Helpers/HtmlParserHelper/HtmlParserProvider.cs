using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.Ayby;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Kufar;
using GodelMastery.FleaMarket.HtmlParser;
using GodelMastery.FleaMarket.HtmlParserInterfaces;
using NLog;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper
{
    public class HtmlParserProvider : IHtmlParserProvider
    {
        private readonly ILotModelFactory lotFactory;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public HtmlParserProvider(ILotModelFactory lotModelFactory)
        {
            lotFactory = lotModelFactory;
        }

        public async Task<List<LotDto>> GetLotsByFilter(string filterContent)
        {
            try
            {
                var lots = new List<LotDto>();
                logger.Info($"Started HtmlParserProvider with filter content \"{filterContent}\"");
                var kufarParser = new HtmlParser<List<HtmlLot>>(new KufarParser(), new KufarSettings(filterContent));
                var aybyParser = new HtmlParser<List<HtmlLot>>(new AyParser(), new AySettings(filterContent));
                var kufarHtmlPages = kufarParser.GetHtmlPages();
                var aybyHtmlPages = aybyParser.GetHtmlPages();
                await Task.WhenAll(kufarHtmlPages, aybyHtmlPages);
                var kufarLots = kufarHtmlPages.Result
                    .SelectMany(listHtmlLots => listHtmlLots.Select(lot => lotFactory.CreateLotDto(lot)))
                    .ToList();
                lots.AddRange(kufarLots);
                var aybyLots = aybyHtmlPages.Result
                    .SelectMany(listHtmlLots => listHtmlLots.Select(lot => lotFactory.CreateLotDto(lot)))
                    .ToList();
                lots.AddRange(aybyLots);
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
