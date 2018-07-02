using System;
using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.DAL.Models.Entities;

namespace GodelMastery.FleaMarket.BL.Core.ModelFactories.Implementations
{
    public class UserModelFactory : IUserModelFactory
    {
        public UserDto CreateUserDto(ApplicationUser applicationUser)
        {
            var userDto = new UserDto
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
            };
            if (applicationUser.Icon != null)
            {
                userDto.Icon = $"data:image/jpg;base64,{Convert.ToBase64String(applicationUser.Icon)}";
            }
            return userDto;
        }
    }
}
