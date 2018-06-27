using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using NLog;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class FilterService : BaseService, IFilterService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public FilterService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
