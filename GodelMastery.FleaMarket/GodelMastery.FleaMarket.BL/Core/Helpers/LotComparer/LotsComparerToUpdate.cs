using GodelMastery.FleaMarket.DAL.Models.Entities;
using System.Collections.Generic;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.LotComparer
{
    public class LotsComparerToUpdate : EqualityComparer<Lot>
    {
        public override int GetHashCode(Lot lot)
        {
            int lotHash = lot.SourceId.GetHashCode() ^ lot.Price.GetHashCode();

            return lotHash.GetHashCode();
        }
        public override bool Equals(Lot lotFromDB, Lot lotFromParser)
        {
            return lotFromDB.SourceId.Equals(lotFromParser.SourceId) && lotFromDB.Price.Equals(lotFromParser.Price);
        }
    }
}
