using GodelMastery.FleaMarket.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.BusinessModels
{
    public class NewLotDtosModel
    {
        public FilterDto FilterDto { get; set; }
        public IEnumerable<LotDto> FreshLots { get; set; }
    }
}
