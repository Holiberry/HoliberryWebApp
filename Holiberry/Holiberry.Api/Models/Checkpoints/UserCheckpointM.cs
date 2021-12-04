using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Checkpoints
{
    public class UserCheckpointM : EntityBaseM
    {
        public string Description { get; set; }

        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public Point Position => Lat != null && Lng != null ? new Point(Lng.Value, Lat.Value) { SRID = 4326 } : null;


        public long UserId { get; set; }
        public UserM User { get; set; }
        

        public virtual ICollection<CheckpointCapturerM> Capturers { get; set; }
    }
}
