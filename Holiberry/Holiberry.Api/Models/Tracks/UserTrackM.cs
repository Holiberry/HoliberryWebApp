using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Tracks
{
    public class UserTrackM : EntityBaseM
    {
        public TrackDestinationTypeE DestinationType { get; set; }
        public TrackTransportTypeE TransportType { get; set; }

        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset? DateFinish { get; set; }


        public TrackStatusE Status { get; set; }

        public long UserId { get; set; }
        public UserM User { get; set; }
    }
}
