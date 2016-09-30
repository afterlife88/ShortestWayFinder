namespace ShortestWayFinder.Domain.DatabaseModels
{
    public class Path
    {
        public int Id { get; set; }
        public string FirstPoint { get; set; }
        public string SecondPoint { get; set; }
        // in minutes
        public int EstimatingTime { get; set; }
    }
}
