using System.Collections.Generic;
using GodelMastery.FleaMarket.BL.BusinessModels;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts
{
    public class NotificationMessageContext : IMessageContext
    {
        public readonly IEnumerable<NewLotDtosModel> newLotDtosModels;

        public NotificationMessageContext(IEnumerable<NewLotDtosModel> newLotDtosModels)
        {
            this.newLotDtosModels = newLotDtosModels;
        }
    }
}
