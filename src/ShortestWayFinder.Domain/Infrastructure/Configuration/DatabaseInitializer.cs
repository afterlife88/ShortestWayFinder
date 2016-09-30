using System;
using System.Threading.Tasks;
using ShortestWayFinder.Domain.Infrastructure.Contracts;

namespace ShortestWayFinder.Domain.Infrastructure.Configuration
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        public Task Seed()
        {
            throw new NotImplementedException();
        }
    }
}
