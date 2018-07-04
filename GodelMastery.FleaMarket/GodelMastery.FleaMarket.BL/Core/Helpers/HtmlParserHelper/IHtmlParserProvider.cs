using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Dtos;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.HtmlParserHelper
{
    interface IHtmlParserProvider
    {
        ICollection<LotDto> GetLotsByFilter(FilterDto filterDto);
    }
}
