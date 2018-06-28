using System.Configuration;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings.Section
{
    public class EmailProviderInfo : ConfigurationElement
    {
        [ConfigurationProperty("email", IsRequired = true)]
        public string Email
        {
            get { return (string)base["email"]; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)base["password"]; }
        }

        [ConfigurationProperty("host", IsRequired = true)]
        public string SmtpHost
        {
            get { return (string)base["host"]; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public int SmtpPort
        {
            get { return (int)base["port"]; }
        }
    }
}
