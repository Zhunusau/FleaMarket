using System.Collections.Generic;
using System.Linq;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Implementations
{
    public class FilterModelFactory : IFilterModelFactory
    {
        public IEnumerable<FilterDto> CreateFilterDtos(IEnumerable<Filter> filters)
        {
            return filters.Select(x => CreateFilterDto(x));
        }

        public FilterDto CreateFilterDto(Filter filter)
        {
            return new FilterDto
            {
                Id = filter.Id,
                ApplicationUserId = filter.ApplicationUserId,
                FilterName = filter.FilterName,
                Content = filter.Content
            };
        }
    }
}
