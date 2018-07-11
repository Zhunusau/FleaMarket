using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Interfaces
{
    public interface ISchedulerService
    {
        void InitializeUserSchedulers();
        Task ChangeLotUpdateInterval(string login, string updateInterval);
    }
}
