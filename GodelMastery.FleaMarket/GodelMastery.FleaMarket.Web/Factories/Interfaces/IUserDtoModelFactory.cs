using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Factories.Interfaces
{
    public interface IUserDtoModelFactory
    {
        UserDto CreateUserDto(SignUpViewModel signUpViewModel);
    }
}
