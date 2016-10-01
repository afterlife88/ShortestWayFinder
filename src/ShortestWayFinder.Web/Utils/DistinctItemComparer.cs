using System.Collections.Generic;
using ShortestWayFinder.Web.Models;

namespace ShortestWayFinder.Web.Utils
{
    public class DistinctItemComparer : IEqualityComparer<PointDto>
    {
        public bool Equals(PointDto x, PointDto y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(PointDto obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
