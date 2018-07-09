using GodelMastery.FleaMarket.BL.BusinessModels;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using NLog;
using System;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class DashboardService : BaseService, IDashboardService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IDashboardModelFactory dashboardModelFactory;
        private readonly IFilterService filterService;
        private readonly IUserService userService;

        public DashboardService(
            IUnitOfWork unitOfWork,
            IFilterService filterService,
            IUserService userService,
            IDashboardModelFactory dashboardModelFactory)
            : base(unitOfWork)
        {
            this.filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.dashboardModelFactory = dashboardModelFactory ?? throw new ArgumentNullException(nameof(dashboardModelFactory));
        }

        public async Task<DashboardModel> GetContent(string login)
        {
            logger.Info("GetContent {0}", login);
            var userFilters = await filterService.GetFilterDtos(login);
            var userInfo = await userService.GetUserDto(login);
            return dashboardModelFactory.CreateDashboardModel(userFilters, userInfo);
        }
    }
}
