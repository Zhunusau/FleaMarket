using System;
using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using NLog;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class LotService : BaseService, ILotService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ILotModelFactory lotModelFactory;

        public LotService(IUnitOfWork unitOfWork, ILotModelFactory lotModelFactory) : base(unitOfWork)
        {
            this.lotModelFactory = lotModelFactory ?? throw new ArgumentNullException(nameof(lotModelFactory));
        }

        public IEnumerable<LotDto> GetLotDtos(int filterId)
        {
            var filter = unitOfWork.Filters.GetById(filterId);
            if (filter == null)
            {
                throw new NullReferenceException(nameof(filter));
            }
            logger.Info($"Get a collection of lots by filter id {filterId}");
            return lotModelFactory.CreateLotDtos(filter.Lots);
        }
    }
}
