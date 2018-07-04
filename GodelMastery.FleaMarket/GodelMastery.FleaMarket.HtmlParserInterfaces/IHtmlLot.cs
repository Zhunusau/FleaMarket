using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.HtmlParserInterfaces
{
    public interface IHtmlLot
    {
        int SourceId { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
        string Location { get; set; }
        string Link { get; set; }
        byte[] Image { get; set; }
        DateTime DateOfFound { get; set; }
        DateTime DateOfUpdate { get; set; }
    }
}
