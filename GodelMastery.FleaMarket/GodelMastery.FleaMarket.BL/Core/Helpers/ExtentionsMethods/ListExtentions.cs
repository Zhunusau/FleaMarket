using GodelMastery.FleaMarket.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodelMastery.FleaMarket.BL.Core.Helpers.ExtentionsMethods
{
    public static class ListExtentions
    {
        public static IEnumerable<TModel> Except<TModel, TComparer>(
            this IEnumerable<TModel> lotsFromParser, 
            IEnumerable<TModel> lotsFromDB) where TModel : class where TComparer : IEqualityComparer<TModel>, new()
        {
            return lotsFromParser.Except(lotsFromDB, new TComparer());
        }
    }
}
