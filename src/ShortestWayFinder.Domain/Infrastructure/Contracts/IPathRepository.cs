using System.Collections.Generic;
using System.Threading.Tasks;
using ShortestWayFinder.Domain.DatabaseModels;

namespace ShortestWayFinder.Domain.Infrastructure.Contracts
{
    public interface IPathRepository
    {
        Task<IEnumerable<Path>> GetAllAsync();
        Task<Path> GetByPointsNamesAsync(string firstPointName, string secondPointName);
        Task<Path> GetAsync(int id);
        Task<int> AddPathAsync(Path model);
        Task<int> RemoveAsync(Path model);
    }
}
