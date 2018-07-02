using GodelMastery.FleaMarket.BL.Core.ModelFactories.Interfaces;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using NLog;
using System;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class UserService : BaseService, IUserService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUserModelFactory userModelFactory;

        public UserService(IUnitOfWork unitOfWork, IUserModelFactory userModelFactory) : base(unitOfWork)
        {
            this.userModelFactory = userModelFactory ?? throw new ArgumentNullException(nameof(userModelFactory));
        }

        public async Task<UserDto> GetUserDto(string login)
        {
            var applicationUser = await unitOfWork.UserManager.FindByEmailAsync(login);
            if (applicationUser == null)
            {
                throw new ArgumentNullException(nameof(applicationUser));
            }
            logger.Info("GetUserInfo {0}", login);
            return userModelFactory.CreateUserDto(applicationUser);
        }
    }
}
