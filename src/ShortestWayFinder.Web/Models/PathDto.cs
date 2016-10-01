using System.ComponentModel.DataAnnotations;

namespace ShortestWayFinder.Web.Models
{
    public class PathDto
    {
        public int? Id { get; set; }
        [Required]
        public string FirstPoint { get; set; }
        [Required]
        public string SecondPoint { get; set; }
        [Required]
        public int Time { get; set; }
    }
}
