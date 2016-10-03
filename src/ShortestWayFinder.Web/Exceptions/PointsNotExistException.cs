using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortestWayFinder.Web.Exceptions
{
    public class PointsNotExistException : Exception
    {
        public PointsNotExistException(string message) : base(message)
        { }
    }
}
