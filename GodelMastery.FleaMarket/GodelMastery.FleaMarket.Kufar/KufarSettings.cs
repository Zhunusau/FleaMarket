using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.Kufar
{
    public class KufarSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://www.kufar.by";
        public string Region { get; set; } = "беларусь";
        public string Category { get; set; } = "продается";
        public string Params { get; set; } = "cu=BYR&o={currentPage}";
        public string Filter { get; set; }
        public string Prefix { get; set; }

        public KufarSettings(string filter)
        {
            Filter = filter;
            Prefix = $"{Region}/{Filter}--{Category}?{Params}";
        }
    }
}
