using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces
{
    public interface IUserModelFactory
    {
        UserDto CreateUserDto(ApplicationUser applicationUser);
    }
}
