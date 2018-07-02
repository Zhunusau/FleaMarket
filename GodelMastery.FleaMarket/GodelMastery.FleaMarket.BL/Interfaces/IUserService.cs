using GodelMastery.FleaMarket.BL.Dtos;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserDto(string login);
    }
}
