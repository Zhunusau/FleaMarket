using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using NLog;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class FilterService : BaseService, IFilterService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IFilterModelFactory filterModelFactory;

        public FilterService(IUnitOfWork unitOfWork, IFilterModelFactory filterModelFactory) : base(unitOfWork)
        {
            this.filterModelFactory = filterModelFactory ?? throw new ArgumentNullException(nameof(filterModelFactory));
        }

        public async Task<IEnumerable<FilterDto>> GetFilterDtos(string login)
        {
            var applicationUser = await unitOfWork.UserManager.FindByEmailAsync(login);
            if (applicationUser == null)
            {
                throw new ArgumentNullException(nameof(applicationUser));
            }
            logger.Info("GetUserFilters {0}", login);
            return filterModelFactory.CreateFilterDtos(applicationUser.Filters);
        }
    }
}
