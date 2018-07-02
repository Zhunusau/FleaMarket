using GodelMastery.FleaMarket.BL.BusinessModels;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardModel> GetContent(string login);
    }
}
