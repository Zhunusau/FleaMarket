using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts;
using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.Messages;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper
{
    public interface IEmailProvider
    {
        Task SendMessageAsync(Message message, IMessageContext context);
    }
}
