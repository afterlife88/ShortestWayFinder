using System;

namespace ShortestWayFinder.Web.Exceptions
{
    public class PointsNotExistException : Exception
    {
        public PointsNotExistException(string message) : base(message)
        { }
    }
}
