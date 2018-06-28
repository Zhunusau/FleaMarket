using System.Collections.Generic;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts
{
    public class NotificationMessageContext : IMessageContext
    {
        public Dictionary<string,string> Lots { get; set; }

        public NotificationMessageContext(Dictionary<string, string> lots)
        {
            Lots = lots;
        }
    }
}
