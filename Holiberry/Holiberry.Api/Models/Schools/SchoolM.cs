using Holiberry.Api.Common.DTO;
using Holiberry.Api.Models.Cities;
using Holiberry.Api.Models.Common.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Schools
{
    public class SchoolM : EntityBaseM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; } // todo - enum


        public long? PhotoId { get; set; }
        public SchoolPhotoM Photo { get; set; }

        public long CityId { get; set; }
        public CityM City { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public Point Position => Lat != null && Lng != null ? new Point(Lng.Value, Lat.Value) { SRID = 4326 } : null;
        public string GetPhotoUrl()
        {
            return string.Empty;
        }
    }
}
