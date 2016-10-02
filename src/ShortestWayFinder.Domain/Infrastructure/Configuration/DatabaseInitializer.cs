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

            var paths = new[]
           {
                 //new Path{FirstPoint="Sagrada Familia",SecondPoint="Las Ramblas",EstimatingTime=40},
                 //new Path{FirstPoint="Las Ramblas",SecondPoint="La Pedrera",EstimatingTime=60},
                 //new Path{FirstPoint="La Pedrera",SecondPoint="Sagrada Familia",EstimatingTime=25},

                 //new Path{FirstPoint="Barcelona FC Museum",SecondPoint="Las Ramblas",EstimatingTime=120},
                 //new Path{FirstPoint="Park Guell",SecondPoint="La Pedrera",EstimatingTime=160},
                 //new Path{FirstPoint="Sagrada Familia",SecondPoint="Picasso Museum",EstimatingTime=30},

                 //new Path{FirstPoint="Placa Reial",SecondPoint="Miro Museum",EstimatingTime=70},
                 //new Path{FirstPoint="La Pedrera",SecondPoint="Placa Reial",EstimatingTime=15},
                 //new Path{FirstPoint="Gothic Quarter",SecondPoint="Las Ramblas",EstimatingTime=45},

                 //new Path{FirstPoint="Casa Batllo",SecondPoint="Gothic Quarter",EstimatingTime=30},

                 //new Path{FirstPoint="Barceloneta",SecondPoint="Las Ramblas",EstimatingTime=15},


                 new Path{FirstPoint="Sagrada Familia",SecondPoint="Las Ramblas",EstimatingTime=40},
                 new Path{FirstPoint="Sagrada Familia",SecondPoint="La Pedrera",EstimatingTime=60},
                 new Path{FirstPoint="Sagrada Familia",SecondPoint="Picasso Museum",EstimatingTime=25},
                 new Path{FirstPoint="Las Ramblas",SecondPoint="Gothic Quarter",EstimatingTime=120},
                 new Path{FirstPoint="Las Ramblas",SecondPoint="Barceloneta",EstimatingTime=30},
                 new Path{FirstPoint="Las Ramblas",SecondPoint="La Pedrera",EstimatingTime=30},
                 new Path{FirstPoint="La Pedrera",SecondPoint="Barceloneta",EstimatingTime=120},
                 new Path{FirstPoint="La Pedrera",SecondPoint="Placa Reial",EstimatingTime=160},
                 new Path{FirstPoint="La Pedrera",SecondPoint="Picasso Museum",EstimatingTime=30},
                 new Path{FirstPoint="Picasso Museum",SecondPoint="Placa Reial",EstimatingTime=30},
                 new Path{FirstPoint="Barceloneta",SecondPoint="Placa Reial",EstimatingTime=15},
                 new Path{FirstPoint="Barceloneta",SecondPoint="Barcelona FC Museum",EstimatingTime=140},
                 new Path{FirstPoint="Placa Reial",SecondPoint="Barcelona FC Museum",EstimatingTime=180},
                 new Path{FirstPoint="Gothic Quarter",SecondPoint="Barcelona FC Museum",EstimatingTime=200},
                 new Path{FirstPoint="Park Guell",SecondPoint="La Pedrera",EstimatingTime=160},
                 new Path{FirstPoint="Park Guell",SecondPoint="Barceloneta",EstimatingTime=180}
             };
            _context.Paths.AddRange(paths);

            await _context.SaveChangesAsync();
        }
    }
}
