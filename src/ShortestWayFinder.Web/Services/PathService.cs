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
        public async Task<bool> CreatePathAsync(PathDto pathDto)
        {

            var checkIsAlreadyPathExist = await _pathRepository.GetByPointsNames(pathDto.PointA, pathDto.PointA);

            if (checkIsAlreadyPathExist != null)
                return false;

            await _pathRepository.AddPath(Mapper.Map<PathDto, Path>(pathDto));

            return true;
        }
    }
}
