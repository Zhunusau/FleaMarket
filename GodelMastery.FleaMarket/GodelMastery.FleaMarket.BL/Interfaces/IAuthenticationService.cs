using System.Security.Claims;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers;
using GodelMastery.FleaMarket.BL.Dtos;

namespace GodelMastery.FleaMarket.BL.Interfaces
{
    public interface IAuthenticationService
    {
        Task<OperationDetails> CreateUser(UserDto userDto);
        Task<string> GenerateEmailConfirmationTokenAsync(string login);
        Task<OperationDetails> ConfirmEmailAsync(string email, string code);
        Task SendMessageAsync(string email, string callBack);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
    }
}
