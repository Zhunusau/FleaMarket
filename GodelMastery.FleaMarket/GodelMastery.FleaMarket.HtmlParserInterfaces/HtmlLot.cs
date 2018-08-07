namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public class HtmlLot : IHtmlLot
    {
        public string SourceId { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string DateOfUpdate { get; set; }
    }
}
