using System.ComponentModel.DataAnnotations;

namespace ShortestWayFinder.Web.Models
{
    public class PathDto
    {
        [Required]
        public string PointA { get; set; }
        [Required]
        public string PointB { get; set; }
        [Required]
        public int Time { get; set; }
    }
}
