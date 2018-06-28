using GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.Messages
{
    public abstract class Message
    {
        public string ToEmailAddress { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }

        protected Message(string toEmailAddress, string title, string subject)
        {
            ToEmailAddress = toEmailAddress;
            Title = title;
            Subject = subject;
        }
        public abstract string GetBody(IMessageContext context);
    }
}
