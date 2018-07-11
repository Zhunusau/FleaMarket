using System.Collections.Generic;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers;
using GodelMastery.FleaMarket.BL.Dtos;
namespace GodelMastery.FleaMarket.BL.Interfaces
{
    public interface IFilterService
    {
        Task<IEnumerable<FilterDto>> GetFilterDtos(string login);
        Task UpdateFilters(string applicationUserId);
        Task RemoveFilter(int id);
        FilterDto GetFilterById(int filterId);
        Task<OperationDetails> Create(FilterDto filterDto);
    }
}
