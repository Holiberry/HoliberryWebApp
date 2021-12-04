using Holiberry.Api.Models.Cities;
using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Users
{
    public class UserHomeM : EntityBaseM
    {
        public long UserId { get; set; }
        public UserM User { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public Point Position => Lat != null && Lng != null ? new Point(Lng.Value, Lat.Value) { SRID = 4326 } : null;

        public long CityId { get; set; }
        public CityM City { get; set; }
    }
}
