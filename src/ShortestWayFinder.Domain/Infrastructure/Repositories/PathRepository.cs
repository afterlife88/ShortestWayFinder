using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShortestWayFinder.Domain.DatabaseModels;
using ShortestWayFinder.Domain.Infrastructure.Contracts;

namespace ShortestWayFinder.Domain.Infrastructure.Repositories
{
    public class PathRepository : IPathRepository
    {
        private readonly DataDbContext _dataDbContext;

        public PathRepository(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public async Task<int> AddPath(Path model)
        {
            _dataDbContext.Paths.Add(model);
            return await _dataDbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Path>> GetAllAsync()
        {
            return await _dataDbContext.Paths.ToArrayAsync();
        }

        public async Task<Path> Get(int id)
        {
            return await _dataDbContext.Paths.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Path> GetByPointsNames(string firstPointName, string secondPointName)
        {
            return
                await
                    _dataDbContext.Paths.FirstOrDefaultAsync(
                        r =>
                            string.Equals(r.FirstPoint, firstPointName, StringComparison.CurrentCultureIgnoreCase) &&
                            string.Equals(r.SecondPoint, secondPointName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
