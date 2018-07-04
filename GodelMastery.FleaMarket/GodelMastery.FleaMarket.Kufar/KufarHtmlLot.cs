using System;
using GodelMastery.FleaMarket.HtmlParserInterfaces;

namespace GodelMastery.FleaMarket.Kufar
{
    public class KufarHtmlLot : IHtmlLot
    {
        public int SourceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public byte[] Image { get; set; }
        public DateTime DateOfFound { get; set; }
        public DateTime DateOfUpdate { get; set; }
    }
}
