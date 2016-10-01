using System.Threading.Tasks;
using ShortestWayFinder.Web.Models;

namespace ShortestWayFinder.Web.Contracts
{
    public interface IPathService
    {
        Task<bool> CreatePathAsync(PathDto pathDto);
    }
}
