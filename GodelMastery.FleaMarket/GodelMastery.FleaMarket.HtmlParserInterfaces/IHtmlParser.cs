namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public interface IHtmlParser<T> where T : class
    {
        bool IsActive { get; }
        IParser<T> Parser { get; set; }
        IParserSettings Settings { get; set; }
    }
}
