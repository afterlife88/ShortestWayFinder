using System;
using System.Linq;
using System.Threading.Tasks;
using ShortestWayFinder.Domain.DatabaseModels;
using ShortestWayFinder.Domain.Infrastructure.Contracts;

namespace ShortestWayFinder.Domain.Infrastructure.Configuration
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly DataDbContext _context;

        public DatabaseInitializer(DataDbContext context)
        {
            _context = context;
        }
        public async Task Seed()
        {
            if (_context.Paths.Any())
            {
                foreach (var item in _context.Paths)
                    _context.Remove(item);
                _context.SaveChanges();
            }

            // var paths = new[]
            //{
            //     new Path{FirstPoint="o",SecondPoint="a",EstimatingTime=2},
            //     new Path{FirstPoint="o",SecondPoint="b",EstimatingTime=5},
            //     new Path{FirstPoint="o",SecondPoint="c",EstimatingTime=4},

            //     new Path{FirstPoint="a",SecondPoint="f",EstimatingTime=12},
            //     new Path{FirstPoint="a",SecondPoint="d",EstimatingTime=7},
            //     new Path{FirstPoint="a",SecondPoint="b",EstimatingTime=2},

            //     new Path{FirstPoint="b",SecondPoint="d",EstimatingTime=4},
            //     new Path{FirstPoint="b",SecondPoint="e",EstimatingTime=3},
            //     new Path{FirstPoint="b",SecondPoint="c",EstimatingTime=1},

            //     new Path{FirstPoint="c",SecondPoint="e",EstimatingTime=4},

            //     new Path{FirstPoint="d",SecondPoint="e",EstimatingTime=1},
            //     new Path{FirstPoint="d",SecondPoint="t",EstimatingTime=5},

            //     new Path{FirstPoint="e",SecondPoint="t",EstimatingTime=7},

            //     new Path{FirstPoint="f",SecondPoint="t",EstimatingTime=3}
            // };
            var paths = new[]
           {
                 new Path{FirstPoint="a",SecondPoint="b",EstimatingTime=1},
                 new Path{FirstPoint="b",SecondPoint="c",EstimatingTime=1000},
                 new Path{FirstPoint="c",SecondPoint="d",EstimatingTime=1},
                 new Path{FirstPoint="d",SecondPoint="a",EstimatingTime=100},
                 new Path{FirstPoint="x",SecondPoint="x",EstimatingTime=100}
             };
            _context.Paths.AddRange(paths);

            await _context.SaveChangesAsync();
        }
    }
}
