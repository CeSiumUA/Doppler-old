using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doppler.API.Storage
{
    public static class CollectionHelper
    {
        public static IQueryable<T> SkipTake<T>(this IQueryable<T> sourceList, int? skip = null, int? take = null)
        {
            if (skip.HasValue)
            {
                sourceList = sourceList.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                sourceList = sourceList.Take(take.Value);
            }

            return sourceList;
        }
    }
}
