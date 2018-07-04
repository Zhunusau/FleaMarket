using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Dtos
{
    public class LotDto
    {
        public int Id { get; set; }
        public int FilterId { get; set; }
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
