using System.Collections.Generic;
using System.Linq;
using ShortestWayFinder.Domain.GraphEntities;
using ShortestWayFinder.Domain.Infrastructure.Contracts;

namespace ShortestWayFinder.Domain.Infrastructure.Algorithms
{
    public class ShortestPathAlgorithm : IShortestPath
    {
        #region Member Variables

        Dictionary<string, Node> _matrix;
        HashSet<Node> _visitedNodes;

        #endregion

        #region Constructor

        public ShortestPathAlgorithm(IEnumerable<Edge> graph)
        {
            _matrix = CreateMatrix(graph);
            _visitedNodes = new HashSet<Node>();
        }

        #endregion

        #region Public Methods

        IList<List<Edge>> IShortestPath.GetShortestPath(string source, string destination)
        {
            _matrix[source].CostFromSource = 0;
            _visitedNodes.Add(_matrix[source]);
            Node destinationNode = _matrix[destination];
            Node minimumNode = new Node();
            var edges = new List<List<Edge>>();

            while (!_visitedNodes.Contains(destinationNode))
            {
                minimumNode = GetMinimumNode();

                if (minimumNode.Label == null)//No Path exists
                {
                    return edges;
                }

                _visitedNodes.Add(minimumNode);
            }

            edges.Add(new List<Edge>());
            GetPaths(minimumNode, edges, edges.Count - 1);

            return edges;
        }

        #endregion

        #region Public Properties

        Dictionary<string, Node> IShortestPath.Matrix
        {
            get
            {
                return _matrix;
            }
            set
            {
                _matrix = value;
            }
        }

        #endregion

        #region Private Methods

        private Dictionary<string, Node> CreateMatrix(IEnumerable<Edge> graph)
        {
            var matrix = new Dictionary<string, Node>();

            foreach (var edge in graph)
            {
                Node firstVertex;

                if (!matrix.ContainsKey(edge.Start))
                {
                    firstVertex = new Node { Label = edge.Start, Neighbors = new Dictionary<string, int>() };
                    matrix.Add(edge.Start, firstVertex);
                }

                firstVertex = matrix[edge.Start];

                Node secondVertex;

                if (!matrix.ContainsKey(edge.End))
                {
                    secondVertex = new Node { Label = edge.End, Neighbors = new Dictionary<string, int>() };
                    matrix.Add(edge.End, secondVertex);
                }

                secondVertex = matrix[edge.End];

                if (!firstVertex.Neighbors.ContainsKey(secondVertex.Label))
                {
                    firstVertex.Neighbors.Add(secondVertex.Label, edge.Cost);
                }
                if (!secondVertex.Neighbors.ContainsKey(firstVertex.Label))
                {
                    secondVertex.Neighbors.Add(firstVertex.Label, edge.Cost);
                }
            }

            return matrix;
        }

        private Node GetMinimumNode()
        {
            var minimumNodeSoFar = new Node();

            foreach (Node currentNode in _visitedNodes)
            {
                IEnumerable<Node> unVisitedNeighbors = GetUnVisitedNeighbors(currentNode);

                var visitedNeighbors = unVisitedNeighbors as Node[] ?? unVisitedNeighbors.ToArray();
                if (visitedNeighbors.Any())
                {
                    RelaxNeighboringNodes(currentNode, visitedNeighbors);

                    Node minimumNodeEndingHere = visitedNeighbors.Aggregate((a, b) => a.CostFromSource < b.CostFromSource ? a : b);

                    if (!minimumNodeSoFar.CostFromSource.HasValue || minimumNodeSoFar.CostFromSource > minimumNodeEndingHere.CostFromSource)
                    {
                        minimumNodeSoFar = minimumNodeEndingHere;
                    }
                }
            }

            return minimumNodeSoFar;
        }

        private IEnumerable<Node> GetUnVisitedNeighbors(Node currentNode)
        {
            IEnumerable<Node> unVisitedNeighbors = currentNode.Neighbors
                .Where(n => !_visitedNodes.Contains(new Node { Label = n.Key }))
                .Select(n => _matrix[n.Key]);
            return unVisitedNeighbors;
        }

        private void RelaxNeighboringNodes(Node currentNode, IEnumerable<Node> unVisitedNeighbors)
        {
            foreach (Node neighbor in unVisitedNeighbors)
            {
                int? newCost = currentNode.CostFromSource + currentNode.Neighbors[neighbor.Label];

                if (neighbor.CostFromSource.HasValue)
                {
                    if (newCost < neighbor.CostFromSource)
                    {
                        neighbor.CostFromSource = newCost;
                        neighbor.Parents = new HashSet<Node> { currentNode };
                    }
                    else if (newCost == neighbor.CostFromSource && !neighbor.Parents.Contains(currentNode))//in case of tie
                    {
                        neighbor.Parents.Add(currentNode);
                    }
                }
                else
                {
                    neighbor.CostFromSource = newCost;
                    neighbor.Parents = new HashSet<Node> { currentNode };
                }
            }
        }

        private void GetPaths(Node destination, List<List<Edge>> edges, int level)
        {
            if (destination.Parents == null)
            {
                return;
            }

            int i = 0;
            List<Edge> routeCopy = new List<Edge>(edges[level]);//make a copy of the current route

            foreach (Node originNode in destination.Parents)
            {
                if (i > 0)//only branch after first edge, as first edge is added to default route
                {
                    edges.Add(routeCopy);
                    level++;
                }

                var edge = new Edge
                {
                    Start = originNode.Label,
                    End = destination.Label,
                    Cost = _matrix[originNode.Label].Neighbors[destination.Label]
                };

                edges[level].Add(edge);
                GetPaths(originNode, edges, level);
                i++;
            }
        }

        #endregion
    }
}
