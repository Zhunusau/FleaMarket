namespace GodelMastery.FleaMarket.Web.Helpers
{
    public static class DateTimeHelper
    {
        public static string GetInterval(int timeInterval)
        {
            string[] sizes = { "min", "h" };
            int order = 0;
            while (timeInterval >= 60)
            {
                order++;
                timeInterval = timeInterval / 60;
            }
            return string.Format($"{timeInterval} {sizes[order]}");
        }
    }
}