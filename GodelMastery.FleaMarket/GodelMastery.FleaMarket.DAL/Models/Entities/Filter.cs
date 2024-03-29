﻿using System.Collections.Generic;

namespace GodelMastery.FleaMarket.DAL.Models.Entities
{
    public class Filter : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string FilterName { get; set; }
        public string Content { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }

        public Filter()
        {
            Lots = new List<Lot>();
        }
    }
}