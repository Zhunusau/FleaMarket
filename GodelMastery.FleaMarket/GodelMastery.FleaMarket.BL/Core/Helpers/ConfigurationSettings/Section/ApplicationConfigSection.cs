using System.Configuration;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings.Section
{
    public class ApplicationConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("EmailProviderInfo", IsRequired = true)]
        public EmailProviderInfo EmailProviderInfo
        {
            get
            {
                return base["EmailProviderInfo"] as EmailProviderInfo;
            }
        }
    }
}
