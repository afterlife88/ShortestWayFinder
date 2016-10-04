using System;

namespace ShortestWayFinder.Web.Exceptions
{
    public class PointsNotConnectedException : Exception
    {
        public PointsNotConnectedException(string message) : base(message)
        { }
    }
}
