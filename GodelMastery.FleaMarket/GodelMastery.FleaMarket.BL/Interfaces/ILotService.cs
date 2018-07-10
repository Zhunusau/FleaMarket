using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Dtos;

namespace GodelMastery.FleaMarket.BL.Interfaces
{
    public interface ILotService
    {
        IEnumerable<LotDto> GetLotDtos(int filterId);
    }
}
