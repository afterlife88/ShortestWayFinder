using System.Collections.Generic;
using ShortestWayFinder.Domain.Entities;

namespace ShortestWayFinder.Domain.Infrastructure.Contracts
{
    public interface IShortestPath
    {
        IList<List<Edge>> GetShortestPath(string source, string destination);
        Dictionary<string, Node> Matrix { get; set; }
    }
}
