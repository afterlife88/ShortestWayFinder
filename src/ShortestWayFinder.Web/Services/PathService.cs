using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ShortestWayFinder.Domain.DatabaseModels;
using ShortestWayFinder.Domain.Infrastructure.Contracts;
using ShortestWayFinder.Web.Contracts;
using ShortestWayFinder.Web.Models;

namespace ShortestWayFinder.Web.Services
{
    public class PathService : IPathService
    {
        private readonly IPathRepository _pathRepository;

        public PathService(IPathRepository pathRepository)
        {
            _pathRepository = pathRepository;
        }
        public async Task<IEnumerable<PathDto>> GetAllExistedPathsAsync()
        {
            var paths = await _pathRepository.GetAllAsync();

            return Mapper.Map<IEnumerable<Path>, IEnumerable<PathDto>>(paths);
        }
        public async Task<bool> CreatePathAsync(PathDto pathDto)
        {

            var checkIsAlreadyPathExist = await _pathRepository.GetByPointsNamesAsync(pathDto.FirstPoint, pathDto.SecondPoint);

            if (checkIsAlreadyPathExist != null)
                return false;

            await _pathRepository.AddPathAsync(Mapper.Map<PathDto, Path>(pathDto));

            return true;
        }
        public async Task<bool> RemovePathAsync(int id)
        {
            var getPathItem = await _pathRepository.GetAsync(id);
            if (getPathItem == null)
                return false;

            await _pathRepository.RemoveAsync(getPathItem);
            return true;
        }

    }
}
