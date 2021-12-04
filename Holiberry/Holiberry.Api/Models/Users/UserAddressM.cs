using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using NetTopologySuite.Geometries;

namespace Holiberry.Api.Models.Users
{
    public class UserAddressM : EntityBaseM
    {
        public long UserId { get; set; }
        public UserM User { get; set; }


        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public Point Position => Lat != null && Lng != null ? new Point(Lng.Value, Lat.Value) { SRID = 4326 } : null;
    }
}