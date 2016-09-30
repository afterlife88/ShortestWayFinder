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
            var edges = new[]
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

            // Expecting shortest path: o=>a=>b=>d=>t
            // You can view graphic result:
            // https://s16.postimg.org/jv5lzfxr9/scenario_A.png

            //Assert
            // 4 edges are in short path
            Assert.Equal(4, path[0].Count);
            // Sum of cost (time) on edges to shortest path 
            // 2+2+4+5=13
            Assert.Equal(13, path[0].Sum(e => e.Cost));
            // 5 Vertex are contains on short path
            Assert.Equal(5, path[1].Count);
            Assert.Equal(13, path[1].Sum(e => e.Cost));
        }

        [Fact]
        public void GetShortestPath_ScenarioBTest()
        {
            //Arrange
            var edges = new[]
            {
                new Edge{Start="a",End="b",Cost=1},
                new Edge{Start="b",End="c",Cost=1000},
                new Edge{Start="c",End="d",Cost=1},
                new Edge{Start="d",End="a",Cost=100},
            };
            IShortestPath targt = new ShortestPath(edges);

            //Act
            IList<List<Edge>> path = targt.GetShortestPath("a", "c");

            // Expecting shortest path: a=>d=>c
            // You can view graphic result:
            // https://s12.postimg.org/qpcibaoy5/scenario_B.png
            //Assert
            Assert.Equal(1, path.Count);

            // 2 edges are in short path
            Assert.Equal(2, path[0].Count);

            // Sum of cost (time) on edges to shortest path 
            // 100 + 1
            Assert.Equal(101, path[0].Sum(e => e.Cost));
        }
    }
}
