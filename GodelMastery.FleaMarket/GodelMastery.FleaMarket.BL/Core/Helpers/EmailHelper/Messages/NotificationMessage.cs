using System;
using System.Linq;
using System.Text;
using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.Messages
{
    public class NotificationMessage : Message
    {
        public NotificationMessage(string toEmailAddress, string title, string subject) 
            : base(toEmailAddress, title, subject) { }

        public override string GetBody(IMessageContext context)
        {
            var notificationMessageContext = context as NotificationMessageContext;
            if (notificationMessageContext == null)
            {
                throw new NullReferenceException(nameof(notificationMessageContext));
            }
            var linkBuilder = new StringBuilder();
            linkBuilder.Append("We found fresh lots for you\n");
            foreach (var filter in notificationMessageContext.newLotDtosModels.Select(x => x.FilterDto))
            {
                linkBuilder.Append($"<ul> <strong><h4>{filter.FilterName}</h4></strong>");
                foreach (var freshLot in notificationMessageContext.newLotDtosModels.SelectMany(x => x.FreshLots).Where(x => x.FilterId.Equals(filter.Id)))
                {
                    linkBuilder.Append("<li>");
                    linkBuilder.Append($"{freshLot.Name} - <a href={freshLot.Link}>Link</a>\n");
                    linkBuilder.Append("</li>");
                }
                linkBuilder.Append("</ul>");
            }
            return linkBuilder.ToString();
        }
    }
}
