using Microsoft.EntityFrameworkCore;
using ShortestWayFinder.Domain.DatabaseModels;

namespace ShortestWayFinder.Domain
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        public DbSet<Path> Paths { get; set; }
    }
}
