using System;
using System.Configuration;
using GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings.Section;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings
{
    public class ConfigProvider : IConfigProvider
    {
        public SendMessageConfigModel ConfigurateSendMessageConfigModel()
        {
            var applicationConfigSection = GetSection("ApplicationConfigSection") as ApplicationConfigSection;
            if (applicationConfigSection == null)
            {
                throw new ArgumentNullException(nameof(applicationConfigSection));
            }
            return new SendMessageConfigModel
            {
                Email = applicationConfigSection.EmailProviderInfo.Email,
                Password = applicationConfigSection.EmailProviderInfo.Password,
                SmtpHost = applicationConfigSection.EmailProviderInfo.SmtpHost,
                SmtpPort = applicationConfigSection.EmailProviderInfo.SmtpPort
            };
        }

        public object GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName);
        }
    }
}
