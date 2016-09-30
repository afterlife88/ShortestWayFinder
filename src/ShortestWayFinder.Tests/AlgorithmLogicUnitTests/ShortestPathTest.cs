using System.Collections.Generic;
using System.Linq;
using ShortestWayFinder.Domain.Entities;
using ShortestWayFinder.Domain.Infrastructure.Contracts;
using ShortestWayFinder.Domain.Infrastructure.Services;
using Xunit;

namespace ShortestWayFinder.Tests.AlgorithmLogicUnitTests
{
    public class ShortestPathTest
    {
        [Fact]
        public void GetShortestPath_ScenarioATest()
        {
            //Arrange
            IEnumerable<Edge> edges = new[]
            {
                new Edge{Start="o",End="a",Cost=2},
                new Edge{Start="o",End="b",Cost=5},
                new Edge{Start="o",End="c",Cost=4},

                new Edge{Start="a",End="f",Cost=12},
                new Edge{Start="a",End="d",Cost=7},
                new Edge{Start="a",End="b",Cost=2},

                new Edge{Start="b",End="d",Cost=4},
                new Edge{Start="b",End="e",Cost=3},
                new Edge{Start="b",End="c",Cost=1},

                new Edge{Start="c",End="e",Cost=4},

                new Edge{Start="d",End="e",Cost=1},
                new Edge{Start="d",End="t",Cost=5},

                new Edge{Start="e",End="t",Cost=7},

                new Edge{Start="f",End="t",Cost=3}
            };
            IShortestPath targt = new ShortestPath(edges);

            //Act
            IList<List<Edge>> path = targt.GetShortestPath("o", "t");

            //Assert
            Assert.Equal(2, path.Count);

            Assert.Equal(4, path[0].Count);
            Assert.Equal(13, path[0].Sum(e => e.Cost));

            Assert.Equal(5, path[1].Count);
            Assert.Equal(13, path[1].Sum(e => e.Cost));
        }
    }
}
