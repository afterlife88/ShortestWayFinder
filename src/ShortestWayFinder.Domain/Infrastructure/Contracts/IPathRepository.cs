using System.Collections.Generic;
using System.Threading.Tasks;
using ShortestWayFinder.Domain.DatabaseModels;

namespace ShortestWayFinder.Domain.Infrastructure.Contracts
{
    public interface IPathRepository
    {
        Task<IEnumerable<Path>> GetAllAsync();
        Task<Path> GetByPointsNames(string firstPointName, string secondPointName);
        Task<Path> Get(int id);
        Task<int> AddPath(Path model);
    }
}
