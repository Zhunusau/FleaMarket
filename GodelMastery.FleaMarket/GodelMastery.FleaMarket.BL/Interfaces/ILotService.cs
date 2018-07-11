using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Interfaces
{
    public interface ILotService
    {
        Task<NewLotDtosModel> UpdateLots(int filterId);
        IEnumerable<LotDto> GetLotDtos(int filterId);
    }
}