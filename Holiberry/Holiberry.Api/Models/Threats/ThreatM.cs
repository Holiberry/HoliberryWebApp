using Holiberry.Api.Models.Common.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Threats
{
    public class ThreatM : EntityBaseM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ThreatTypeE Type { get; set; }


        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public Point Position => Lat != null && Lng != null ? new Point(Lng.Value, Lat.Value) { SRID = 4326 } : null;

        public long? PhotoId { get; set; }
        public ThreatPhotoM Photo { get; set; }
    }
}
