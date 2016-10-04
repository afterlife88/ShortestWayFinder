using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShortestWayFinder.Domain.DatabaseModels;
using ShortestWayFinder.Domain.GraphEntities;
using ShortestWayFinder.Domain.Infrastructure.Algorithms;
using ShortestWayFinder.Domain.Infrastructure.Contracts;
using ShortestWayFinder.Web.Contracts;
using ShortestWayFinder.Web.Exceptions;
using ShortestWayFinder.Web.Models;
using ShortestWayFinder.Web.Utils;

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
            if (!(pathDto.Time > 0))
                throw new TimeIsNotPositiveException("Time must be a positive number!");

            var checkIsAlreadyPathExist = await _pathRepository.GetByPointsNamesAsync(pathDto.FirstPoint, pathDto.SecondPoint);

            if (checkIsAlreadyPathExist != null)
                return false;

            await _pathRepository.AddPathAsync(Mapper.Map<PathDto, Path>(pathDto));

            return true;
        }
        public async Task<bool> UpdatePathAsync(PathDto pathDto)
        {
            if (!(pathDto.Time > 0))
                throw new TimeIsNotPositiveException("Time must be a positive number!");

            var checkIsAlreadyPathExist = await _pathRepository.GetByPointsNamesAsync(pathDto.FirstPoint, pathDto.SecondPoint);
            if (checkIsAlreadyPathExist != null)
            {
                if (checkIsAlreadyPathExist.Id != pathDto.Id)
                    return false;
            }

            await _pathRepository.EditAsync(pathDto.Id.GetValueOrDefault(-1), Mapper.Map<PathDto, Path>(pathDto));

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
        public async Task<IEnumerable<PathDto>> GetShortestPathAsync(ShortestPathRequestDto requestDto)
        {

            // Check if points exist
            var checkIsPointsExist = await GetPointsAsync();
            var firstOrDefault = checkIsPointsExist.FirstOrDefault(r => r.Name == requestDto.FirstPoint || r.Name == requestDto.SecondPoint);

            if (firstOrDefault == null)
                throw new PointsNotExistException("Requested points not exist!");


            var getAllPaths = await _pathRepository.GetAllAsync();
            IShortestPath targt = new ShortestPathAlgorithm(Mapper.Map<IEnumerable<Path>, IEnumerable<Edge>>(getAllPaths));

            IList<List<Edge>> path = targt.GetShortestPath(requestDto.FirstPoint, requestDto.SecondPoint);

            if (path.Count == 0)
                throw new PointsNotConnectedException(
                    $"Points from {requestDto.FirstPoint} to {requestDto.SecondPoint} are not connected on the map! Please add connections");

            // Reverse just to return list in right sequence from first to second point through other points
            path[0].Reverse();
            return Mapper.Map<IEnumerable<Edge>, IEnumerable<PathDto>>(path[0]);
        }
        public async Task<IEnumerable<PointDto>> GetPointsAsync()
        {
            var allpaths = await _pathRepository.GetAllAsync();

            var listOfPoints = new List<PointDto>();

            foreach (var path in allpaths)
            {
                listOfPoints.Add(new PointDto { Name = path.FirstPoint });
                listOfPoints.Add(new PointDto { Name = path.SecondPoint });
            }
            var result = listOfPoints.Distinct(new DistinctItemComparer());
            return result.ToList();
        }
    }
}
