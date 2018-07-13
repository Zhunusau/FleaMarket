using GodelMastery.FleaMarket.DAL.Models.Entities;
using System.Collections.Generic;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.LotComparer
{
    public class LotsComparerToAddNew : EqualityComparer<Lot>
    {
        public override int GetHashCode(Lot lot)
        {
            int lotHash = lot.SourceId.GetHashCode();

            return lotHash.GetHashCode();
        }
        public override bool Equals(Lot lotFromDB, Lot lotFromParser)
        {
            return lotFromDB.SourceId.Equals(lotFromParser.SourceId);
        }
    }
}
