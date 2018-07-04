using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.HtmlParser
{
    public interface IHtmlLoader
    {
        Task<string> GetSourceByPageId(int page);
    }
}
