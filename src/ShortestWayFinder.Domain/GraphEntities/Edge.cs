namespace ShortestWayFinder.Domain.GraphEntities
{
    public class Edge
    {
        public string Start { get; set; }
        public string End { get; set; }
        /// <summary>
        /// Time beetween points (in minutes)
        /// </summary>
        public int Cost { get; set; }
    }
}
