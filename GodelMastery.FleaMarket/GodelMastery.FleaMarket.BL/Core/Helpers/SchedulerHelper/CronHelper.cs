using Hangfire;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.SchedulerHelper
{
    public static class CronHelper
    {
        public static string GetCronIntervalByMinutes(int interval)
        {
            var result = string.Empty;
            switch (interval)
            {
                case 10:
                    result = Cron.MinuteInterval(10);
                    break;
                case 60:
                    result = Cron.HourInterval(GetFullHoursByMinutes(60));
                    break;
                case 180:
                    result = Cron.HourInterval(GetFullHoursByMinutes(180));
                    break;
                case 720:
                    result = Cron.HourInterval(GetFullHoursByMinutes(720));
                    break;
                case 1440:
                    result = Cron.DayInterval(GetFullDaysByMinutes(1440));
                    break;
            }
            return result;
        }

        #region PrivateMethods
        private static int GetFullDaysByMinutes(int minutes)
        {
            if (minutes > 1380)
            {
                return minutes / 60 / 24;
            }
            return 0;
        }
        private static int GetFullHoursByMinutes(int minutes)
        {
            if (minutes > 59)
            {
                return minutes / 60;
            }
            return 0;
        }
        #endregion
    }
}
