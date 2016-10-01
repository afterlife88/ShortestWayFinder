using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortestWayFinder.Web.Exceptions
{
    public class PointsNotConnectedException : Exception
    {
        public PointsNotConnectedException(string message) : base(message)
        { }
    }
}
