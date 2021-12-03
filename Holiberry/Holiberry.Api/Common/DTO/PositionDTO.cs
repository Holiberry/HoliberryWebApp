using AutoMapper;
using Holiberry.Api.Mappings;
using NetTopologySuite.Geometries;

namespace Holiberry.Api.Common.DTO
{
    public class PositionDTO : IMapFrom<Point>
    {
        public double Lat { get; set; }
        public double Lng { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Point, PositionDTO>()
                .ForMember(d => d.Lat, opt => opt.MapFrom(s => s.Y))
                .ForMember(d => d.Lng, opt => opt.MapFrom(s => s.X));
        }
    }
}