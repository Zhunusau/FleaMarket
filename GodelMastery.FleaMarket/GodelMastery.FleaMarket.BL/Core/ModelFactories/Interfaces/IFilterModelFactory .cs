using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces
{
    public interface IFilterModelFactory
    {
        IEnumerable<FilterDto> CreateFilterDtos(IEnumerable<Filter> filters);
        FilterDto CreateFilterDto(Filter filter);
    }
}
