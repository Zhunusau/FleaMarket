namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public interface IHtmlLot
    {
        string SourceId { get; set; }
        string Name { get; set; }
        string Price { get; set; }
        string Location { get; set; }
        string Link { get; set; }
        string Image { get; set; }
        string DateOfUpdate { get; set; }
    }
}
