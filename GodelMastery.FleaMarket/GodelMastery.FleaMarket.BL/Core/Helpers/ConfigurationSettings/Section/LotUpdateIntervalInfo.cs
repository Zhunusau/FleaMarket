using System.Configuration;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.ConfigurationSettings.Section
{
    public class LotUpdateIntervalInfo : ConfigurationElement
    {
        [ConfigurationProperty("everyTenMinutes", IsRequired = true)]
        public int EveryTenMinutes
        {
            get { return (int)base["everyTenMinutes"]; }
        }

        [ConfigurationProperty("everyHour", IsRequired = true)]
        public int EveryHour
        {
            get { return (int)base["everyHour"]; }
        }

        [ConfigurationProperty("everyThreeHours", IsRequired = true)]
        public int EveryThreeHours
        {
            get { return (int)base["everyThreeHours"]; }
        }

        [ConfigurationProperty("everyTwelveHours", IsRequired = true)]
        public int EveryTwelveHours
        {
            get { return (int)base["everyTwelveHours"]; }
        }

        [ConfigurationProperty("everyDay", IsRequired = true)]
        public int EveryDay
        {
            get { return (int)base["everyDay"]; }
        }
    }
}
