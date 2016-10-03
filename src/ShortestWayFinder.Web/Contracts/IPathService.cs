using System.Collections.Generic;
using System.Threading.Tasks;
using ShortestWayFinder.Web.Models;

namespace ShortestWayFinder.Web.Contracts
{
    public interface IPathService
    {
        Task<bool> CreatePathAsync(PathDto pathDto);
        Task<IEnumerable<PathDto>> GetAllExistedPathsAsync();
        Task<bool> RemovePathAsync(int id);
        Task<IEnumerable<PathDto>> GetShortestPathAsync(ShortestPathRequestDto requestDto);
        Task<IEnumerable<PointDto>> GetPointsAsync();
        Task<bool> UpdatePathAsync(PathDto pathDto);
    }
}
