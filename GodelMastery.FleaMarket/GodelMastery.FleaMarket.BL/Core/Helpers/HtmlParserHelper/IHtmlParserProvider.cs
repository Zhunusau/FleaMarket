using System.Collections.Generic;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Dtos;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper
{
    public interface IHtmlParserProvider
    {
        Task<List<LotDto>> GetLotsByFilter(string filterContent);
    }
}
