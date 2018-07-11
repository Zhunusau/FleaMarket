namespace GodelMastery.FleaMarket.BL.Core.Helpers.SchedulerHelper
{
    public interface ISchedulerManager
    {
        void AddOrUpdateLotUpdateJob(string jobId, int userTimeInterval);
        void DeleteJob(string jobId);
    }
}
