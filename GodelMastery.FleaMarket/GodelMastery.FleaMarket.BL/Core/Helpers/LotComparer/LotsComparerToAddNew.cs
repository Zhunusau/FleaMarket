﻿using GodelMastery.FleaMarket.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.LotComparer
{
    public class LotsComparerToAddNew : EqualityComparer<LotDto>
    {
        public override int GetHashCode(LotDto lot)
        {
            int lotHash = lot.SourceId.GetHashCode();

            return lotHash.GetHashCode();
        }
        public override bool Equals(LotDto lotFromDB, LotDto lotFromParser)
        {
            return lotFromDB.SourceId.Equals(lotFromParser.SourceId);
        }
    }
}