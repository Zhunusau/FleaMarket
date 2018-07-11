using System.Threading.Tasks;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.Messages;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper
{
    public interface IEmailProvider
    {
        Task SendMessageAsync<TMessage, TMessageContext>(TMessage message, TMessageContext context)
            where TMessage : Message
            where TMessageContext : IMessageContext;

        void SendMessage<TMessage, TMessageContext>(TMessage message, TMessageContext context)
            where TMessage : Message
            where TMessageContext : IMessageContext;
    }
}
