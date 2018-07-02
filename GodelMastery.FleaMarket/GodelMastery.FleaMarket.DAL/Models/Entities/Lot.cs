using System;

namespace GodelMastery.FleaMarket.DAL.Models.Entities
{
    public class Lot : IBaseEntity<int>
    {
        public int Id { get; set; }
        public int FilterId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Link { get; set; }
        public byte[] Image { get; set; }
        public DateTime DateOfFound { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
        public virtual Filter Filter { get; set; }
    }
}