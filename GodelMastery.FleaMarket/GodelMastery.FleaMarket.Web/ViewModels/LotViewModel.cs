using System;

namespace GodelMastery.FleaMarket.Web.ViewModels
{
    public class LotViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public string DateOfUpdate { get; set; }
        public int FilterId { get; set; }
    }
}