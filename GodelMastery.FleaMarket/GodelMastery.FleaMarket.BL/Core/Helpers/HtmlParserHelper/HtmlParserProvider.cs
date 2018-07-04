using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper
{
    public class HtmlParserProvider : IHtmlParserProvider
    {
        private readonly IHtmlParser<IEnumerable<IHtmlLot>> htmlParser;

        public HtmlParserProvider(IHtmlParser<IEnumerable<IHtmlLot>> htmlParser)
        {
            this.htmlParser = htmlParser;
        }

        public LotDto lotA = new LotDto { Name = "A" };
        public LotDto lotB = new LotDto { Name = "B" };
        public LotDto lotC = new LotDto { Name = "C" };

        public ICollection<LotDto> GetLotsByFilter(FilterDto filterDto)
        {
            return new List<LotDto> {lotA, lotB, lotC};
        }
    }
}
