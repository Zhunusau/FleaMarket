using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Factories.Interfaces
{
    public interface IUserViewModelFactory
    {
        UserDto CreateUserDto(SignUpViewModel signUpViewModel);
        UserDto CreateUserDto(SignInViewModel signInViewModel);
    }
}
