using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortestWayFinder.Domain.Entities
{
    public class Node
    {
        #region Public Properties

        public string Label { get; set; }
        public int? CostFromSource { get; set; }
        public Dictionary<string, int> Neighbors { get; set; }
        public HashSet<Node> Parents { get; set; }

        #endregion

        #region Public Methods

        public override int GetHashCode()
        {
            return Label.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Node other = obj as Node;

            if (other != null)
            {
                return this.Label == other.Label;
            }

            return false;
        }

        #endregion
    }
}
