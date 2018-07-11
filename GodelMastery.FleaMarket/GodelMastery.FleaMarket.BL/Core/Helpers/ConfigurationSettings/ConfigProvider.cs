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
                throw new NullReferenceException(nameof(applicationConfigSection));
            }
            return new SendMessageConfigModel
            {
                Email = applicationConfigSection.EmailProviderInfo.Email,
                Password = applicationConfigSection.EmailProviderInfo.Password,
                SmtpHost = applicationConfigSection.EmailProviderInfo.SmtpHost,
                SmtpPort = applicationConfigSection.EmailProviderInfo.SmtpPort
            };
        }

        public LotUpdateIntervalConfigModel ConfigurateLotUpdateIntervalConfigModel() 
        {
            var applicationConfigSection = GetSection("ApplicationConfigSection") as ApplicationConfigSection;
            if (applicationConfigSection == null)
            {
                throw new NullReferenceException(nameof(applicationConfigSection));
            }
            return new LotUpdateIntervalConfigModel
            {
                EveryTenMinutes = applicationConfigSection.LotUpdateIntervalInfo.EveryTenMinutes,
                EveryHour = applicationConfigSection.LotUpdateIntervalInfo.EveryHour,
                EveryThreeHours = applicationConfigSection.LotUpdateIntervalInfo.EveryThreeHours,
                EveryTwelveHours = applicationConfigSection.LotUpdateIntervalInfo.EveryTwelveHours,
                EveryDay = applicationConfigSection.LotUpdateIntervalInfo.EveryDay
            };
        }

        public object GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName);
        }
    }
}
