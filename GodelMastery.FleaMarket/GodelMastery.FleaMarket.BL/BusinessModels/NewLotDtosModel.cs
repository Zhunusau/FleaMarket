using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Dtos;

namespace GodelMastery.FleaMarket.BL.BusinessModels
{
    public class NewLotDtosModel
    {
        public FilterDto FilterDto { get; set; }
        public IEnumerable<LotDto> FreshLots { get; set; }
    }
}
