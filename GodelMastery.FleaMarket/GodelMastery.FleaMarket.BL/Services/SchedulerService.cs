using System;
using System.Linq;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers.SchedulerHelper;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using NLog;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class SchedulerService : BaseService, ISchedulerService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly ISchedulerManager schedulerManager;

        public SchedulerService(IUnitOfWork unitOfWork, ISchedulerManager schedulerManager) : base(unitOfWork)
        {
            this.schedulerManager = schedulerManager ?? throw new ArgumentNullException(nameof(schedulerManager));
        }

        public void InitializeUserSchedulers()
        {
            foreach (var applicationUser in unitOfWork.UserManager.Users.ToList())
            {
                if (applicationUser.EmailConfirmed && applicationUser.LotUpdateInterval != null)
                {
                    logger.Info($"Initialize scheduler for {applicationUser.Email} " +
                                $"with user id {applicationUser.Id} and lots update interval {applicationUser.LotUpdateInterval}");
                    schedulerManager.AddOrUpdateLotUpdateJob(applicationUser.Id, applicationUser.LotUpdateInterval.Value);
                }
            }
        }

        public async Task ChangeLotUpdateInterval(string login, string updateInterval)
        {
            if (updateInterval == null)
            {
                throw new ArgumentNullException(nameof(updateInterval));
            }
            var applicationUser = await unitOfWork.UserManager.FindByEmailAsync(login);
            if (applicationUser == null)
            {
                throw new NullReferenceException(nameof(applicationUser));
            }
            logger.Info($"Change the time of lots update for {applicationUser.Email}");
            if (applicationUser.EmailConfirmed)
            {
                if (updateInterval.Equals("None"))
                {
                    logger.Info($"Remove scheduler and set lots update interval to null for {applicationUser.Email}");
                    applicationUser.LotUpdateInterval = null;
                    schedulerManager.DeleteJob(applicationUser.Id);                  
                }
                else
                {
                    var intUpdateInterval = Int32.Parse(updateInterval);
                    logger.Info($"Update scheduler and set lots update interval to {intUpdateInterval} minutes for {applicationUser.Email}");
                    applicationUser.LotUpdateInterval = intUpdateInterval;
                    schedulerManager.AddOrUpdateLotUpdateJob(applicationUser.Id, intUpdateInterval);
                }
                await unitOfWork.UserManager.UpdateAsync(applicationUser);
            }
        }
    }
}
