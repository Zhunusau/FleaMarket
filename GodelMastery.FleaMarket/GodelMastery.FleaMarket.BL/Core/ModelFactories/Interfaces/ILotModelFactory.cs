using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces
{
    public interface ILotModelFactory
    {
        IEnumerable<LotDto> CreateLotDtos(IEnumerable<Lot> lots);
        LotDto CreateLotDto(Lot lot);
        IEnumerable<Lot>  CreateLots(IEnumerable<LotDto> lotDtos);
        Lot CreateLot(LotDto lotDto);
        NewLotDtosModel CreateNewLotDtosModel(FilterDto filterDto, IEnumerable<Lot> lots);
    }
}
