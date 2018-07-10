using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.Messages;
using GodelMastery.FleaMarket.BL.Dtos;
using GodelMastery.FleaMarket.BL.Interfaces;
using GodelMastery.FleaMarket.DAL.Interfaces;
using GodelMastery.FleaMarket.DAL.Models.Entities;
using Microsoft.AspNet.Identity;
using NLog;

namespace GodelMastery.FleaMarket.BL.Services
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IEmailProvider emailProvider;

        public AuthenticationService(IUnitOfWork unitOfWork, IEmailProvider emailProvider) : base(unitOfWork)
        {
            this.emailProvider = emailProvider ?? throw new ArgumentNullException(nameof(emailProvider));
        }

        public async Task<OperationDetails> CreateUser(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
            var isEmailExists = await IsEmailExist(userDto.Email);
            if (isEmailExists)
            {
                return new OperationDetails(false, "User with the same email is already exist", "Email");
            }
            logger.Info("CreateUser {0} {1} {2} {3} {4}", userDto.Email, userDto.Password, userDto.Role, userDto.FirstName, userDto.LastName);
            try
            {
                var appUser = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email, FirstName = userDto.FirstName, LastName = userDto.LastName };
                var result = await unitOfWork.UserManager.CreateAsync(appUser, userDto.Password);
                if (result.Errors.Any())
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }
                await unitOfWork.UserManager.AddToRoleAsync(appUser.Id, userDto.Role);
                return new OperationDetails(true, "Registration successful", "");
            }
            catch (Exception e)
            {
                logger.Error(e.ToString(), "CreateUser exception");
                return new OperationDetails(false, e.Message, "");
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string login)
        {
            var appUser = await unitOfWork.UserManager.FindByEmailAsync(login);
            if (appUser == null)
            {
                throw new ArgumentNullException(nameof(appUser));
            }
            logger.Info("GenerateEmailConfirmationTokenAsync {0}", login);
            var code = await unitOfWork.UserManager.GenerateEmailConfirmationTokenAsync(appUser.Id);
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }
            return code;
        }

        public async Task<OperationDetails> ConfirmEmailAsync(string login, string code)
        {
            var appUser = await unitOfWork.UserManager.FindByEmailAsync(login);
            if (appUser == null)
            {
                throw new ArgumentNullException(nameof(appUser));
            }
            logger.Info("ConfirmEmailAsync {0} {1}", login, code);
            var identityResult = await unitOfWork.UserManager.ConfirmEmailAsync(appUser.Id, code);
            if (identityResult.Succeeded)
            {
                return new OperationDetails(identityResult.Succeeded, "Confirmation successful", "");
            }
            return new OperationDetails(false, "Confirmation failed", "");
        }

        public async Task SendMessageAsync(string email, string callBack)
        {
            await emailProvider.SendMessageAsync(
                new VerificationMessage(email, SendVerificationLinkHelper.Title, SendVerificationLinkHelper.Subject),
                new VerificationMessageContext(callBack));
        }
        
        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
            logger.Info($"Checking credentials for user {userDto.Email}");

            ClaimsIdentity claim = null;

            ApplicationUser user = await unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                logger.Info($"Created identity for: {userDto.Email}");
                claim = await unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        #region PrivateMethods
        private async Task<bool> IsEmailExist(string email) => await unitOfWork.UserManager.FindByEmailAsync(email) != null;
        #endregion
    }
}
