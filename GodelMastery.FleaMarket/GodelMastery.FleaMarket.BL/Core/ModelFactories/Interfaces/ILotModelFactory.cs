using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces
{
    public interface ILotModelFactory
    {
        LotDto CreateLotDto(Lot lot);
        IEnumerable<LotDto> CreateLotDtos(IEnumerable<Lot> lots);
    }
}
