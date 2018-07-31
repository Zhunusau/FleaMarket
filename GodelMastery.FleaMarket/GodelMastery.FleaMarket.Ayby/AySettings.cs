using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.Ayby
{
    public class AySettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "http://ay.by";
        public string Region { get; set; } = "city_id=id";
        public string Category { get; set; } = "sch";
        public string Page { get; set; } = "page={currentPage}";
        public string Prefix { get; set; }

        public AySettings(string filter)
        {
            Prefix = $"{Category}/?kwd={filter}&{Page}";
        }
    }
}
