namespace GodelMastery.FleaMarket.BL.Core.Helpers.EmailHelper.MessageContexts
{
    public class VerificationMessageContext : IMessageContext
    {
        public string VerificationLink { get; set; }

        public VerificationMessageContext(string verificationLink)
        {
            VerificationLink = verificationLink;
        }
    }
}
