using GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings.Section;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings
{
    public interface IConfigProvider
    {
        object GetSection(string sectionName);
        SendMessageConfigModel ConfigurateSendMessageConfigModel();
    }
}
