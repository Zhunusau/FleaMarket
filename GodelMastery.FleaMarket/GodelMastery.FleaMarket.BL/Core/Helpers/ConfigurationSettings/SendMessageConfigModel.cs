namespace GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings
{
    public class SendMessageConfigModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }
    }
}
