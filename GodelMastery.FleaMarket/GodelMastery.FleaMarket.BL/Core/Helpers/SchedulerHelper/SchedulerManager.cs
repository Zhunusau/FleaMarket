using System;
using System.Linq;
using GodelMastery.FleaMarket.BL.Interfaces;
using Hangfire;
using Hangfire.Storage;
using NLog;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.SchedulerHelper
{
    public class SchedulerManager : ISchedulerManager
    {
        private readonly IFilterService filterService;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public SchedulerManager(IFilterService filterService)
        {
            this.filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
        }

        public void AddOrUpdateLotUpdateJob(string jobId, int userTimeInterval)
        {
            var recurringJobDto = GetJobById(jobId);
            if (recurringJobDto == null)
            {
                logger.Info($"Add new lot update job with id {jobId} and interval {userTimeInterval} minutes");
                RecurringJob.AddOrUpdate(jobId, () => filterService.UpdateFilters(jobId), CronHelper.GetCronIntervalByMinutes(userTimeInterval));
            }
            else
            {
                DeleteJob(recurringJobDto.Id);
                logger.Info($"Update existing job with id {recurringJobDto.Id} and interval {userTimeInterval} minutes");
                RecurringJob.AddOrUpdate(recurringJobDto.Id, () => filterService.UpdateFilters(jobId), CronHelper.GetCronIntervalByMinutes(userTimeInterval));
            }
        }

        public void DeleteJob(string jobId)
        {
            var recurringJobDto = GetJobById(jobId);
            if (recurringJobDto != null)
            {
                logger.Info($"Delete existing job with id {jobId}");
                RecurringJob.RemoveIfExists(recurringJobDto.Id);
            }
        }

        private RecurringJobDto GetJobById(string jobId)
        {
            logger.Info($"Get jot with id {jobId}");
            using (var connection = JobStorage.Current.GetConnection())
            {
                return connection.GetRecurringJobs().SingleOrDefault(x => x.Id == jobId);
            }
        }
    }
}
