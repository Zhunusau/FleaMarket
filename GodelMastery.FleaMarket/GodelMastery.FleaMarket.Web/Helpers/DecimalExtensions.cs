namespace GodelMastery.FleaMarket.Web.Helpers
{
    public static class DecimalExtensions
    {
        public static string ToString(this decimal some, bool compactFormat)
        {
            if (compactFormat)
            {
                return some.ToString("F") + " BYN";
            }
            return some.ToString("F");
        }
    }
}