using System;

namespace ShortestWayFinder.Web.Exceptions
{
    public class TimeIsNotPositiveException : Exception
    {
        public TimeIsNotPositiveException(string message) : base(message)
        {
        }
    }
}
