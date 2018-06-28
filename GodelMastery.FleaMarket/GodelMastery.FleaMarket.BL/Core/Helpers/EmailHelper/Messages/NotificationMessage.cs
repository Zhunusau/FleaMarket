using System;
using System.Text;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.Messages
{
    public class NotificationMessage : Message
    {
        public NotificationMessage(string toEmailAddress, string title, string subject) : base(toEmailAddress, title, subject) { }

        public override string GetBody(IMessageContext context)
        {
            var notificationContext = context as NotificationMessageContext;
            if (notificationContext == null)
            {
                throw new ArgumentNullException(nameof(notificationContext));
            }
            var linkBuilder = new StringBuilder();
            linkBuilder.Append("We found fresh lots for you\n");
            linkBuilder.Append("<ul>");
            foreach (var lot in notificationContext.Lots)
            {
                linkBuilder.Append("<li>");
                linkBuilder.Append($"{lot.Value} - {lot.Key}\n");
                linkBuilder.Append("</li>");
            }
            linkBuilder.Append("</ul>");
            return linkBuilder.ToString();
        }
    }
}
