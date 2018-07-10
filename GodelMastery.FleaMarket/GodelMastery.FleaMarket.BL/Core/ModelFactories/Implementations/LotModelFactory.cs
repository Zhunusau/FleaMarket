using System;
using System.Collections.Generic;
using System.Linq;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Implementations
{
    public class LotModelFactory : ILotModelFactory
    {
        public LotDto CreateLotDto(Lot lot)
        {
            return new LotDto
            {
                Name = lot.Name,
                Price = lot.Price,
                Location = lot.Location,
                Image = lot.Image,
                DateOfUpdate = lot.DateOfUpdate,
                FilterId = lot.FilterId,
                Link = lot.Link
            };
        }

        public IEnumerable<LotDto> CreateLotDtos(IEnumerable<Lot> lots) => lots.Select(x => CreateLotDto(x));
    }
}
