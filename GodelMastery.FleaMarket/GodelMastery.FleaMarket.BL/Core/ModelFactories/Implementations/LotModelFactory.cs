using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using GodelMastery.FleaMarket.BL.BusinessModels;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Implementations
{
    public class LotModelFactory : ILotModelFactory
    {
        public Lot CreateLot(LotDto lotDto)
        {
            return new Lot
            {
                FilterId = lotDto.FilterId,
                Name = lotDto.Name,
                Price = lotDto.Price,
                Location = lotDto.Location,
                Image = lotDto.Image,
                Link = lotDto.Link,
                SourceId = lotDto.SourceId,
                DateOfFound = lotDto.DateOfFound,
                DateOfUpdate = lotDto.DateOfUpdate,
                Id = lotDto.Id
            };
        }

        public LotDto CreateLotDto(Lot lot)
        {
            return new LotDto
            {
                FilterId = lot.FilterId,
                Name = lot.Name,
                Price = lot.Price,
                Location = lot.Location,
                Image = lot.Image,
                Link = lot.Link,
                SourceId = lot.SourceId,
                DateOfFound = lot.DateOfFound,
                DateOfUpdate = lot.DateOfUpdate,
                Id = lot.Id
            };
        }

        public IEnumerable<LotDto> CreateLotDtos(IEnumerable<Lot> lots)
        {
            return lots.Select(x => CreateLotDto(x));
        }

        public IEnumerable<Lot> CreateLots(IEnumerable<LotDto> lotDtos)
        {
            return lotDtos.Select(x => CreateLot(x));
        }
        public NewLotDtosModel CreateNewLotDtosModel(FilterDto filterDto, IEnumerable<Lot> lots)
        {
            var freshLots = CreateLotDtos(lots);

            return new NewLotDtosModel
            {
                FilterDto = filterDto,
                FreshLots = freshLots
            };
        }
    }
}
