using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.Web.Factories.Interfaces;
using GodelMastery.FleaMarket.Web.ViewModels;

namespace GodelMastery.FleaMarket.Web.Factories.Implementations
{
    public class UserViewModelFactory : IUserViewModelFactory
    {
        public UserDto CreateUserDto(SignUpViewModel signUpViewModel)
        {
            return new UserDto
            {
                Email = signUpViewModel.Email,
                FirstName = signUpViewModel.FirstName,
                LastName = signUpViewModel.LastName,
                Password = signUpViewModel.Password,
                Role = "User",
            };
        }

        public UserDto CreateUserDto(SignInViewModel signInViewModel)
        {
            return new UserDto
            {
                Email = signInViewModel.Email,
                Password = signInViewModel.Password
            };
        }
    }
}