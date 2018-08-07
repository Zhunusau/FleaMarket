using System;

namespace GodelMastery.FleaMarket.BL.Dtos
{
    public class LotDto
    {
        public int Id { get; set; }
        public int FilterId { get; set; }
        public string SourceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }
        public DateTime DateOfFound { get; set; }
        public string DateOfUpdate { get; set; }
    }
}
