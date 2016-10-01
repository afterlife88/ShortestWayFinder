using AutoMapper;
using ShortestWayFinder.Domain.DatabaseModels;
using ShortestWayFinder.Web.Models;

namespace ShortestWayFinder.Web.Configuration
{
    /// <summary>
    /// Automapper configuration
    /// </summary>
    public class AutomapperConfiguration
    {
        /// <summary>
        /// Configuration of automapper maps
        /// </summary>
        public static void Load()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<PathDto, Path>()
                    .ForMember(dest => dest.FirstPoint, dto => dto.MapFrom(src => src.PointA))
                    .ForMember(dest => dest.SecondPoint, dto => dto.MapFrom(src => src.PointB))
                    .ForMember(dest => dest.EstimatingTime, dto => dto.MapFrom(src => src.Time));
            });
        }
    }
}
