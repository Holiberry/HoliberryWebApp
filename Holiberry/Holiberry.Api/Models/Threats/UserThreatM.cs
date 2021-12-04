using Holiberry.Api.Models.Common.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Threats
{
    public class UserThreatM : EntityBaseM
    {
        public UserThreatTypeE Type { get; set; }


        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public Point Position => Lat != null && Lng != null ? new Point(Lng.Value, Lat.Value) { SRID = 4326 } : null;

        public DateTimeOffset ExpirationDate { get; set; }

        
        public int VotesFor { get; set; }
        public int VotesAgaints { get; set; }

        public virtual ICollection<UserThreatVoterM> Voters { get; set; }





        public void RefreshVotes() 
        {
            if (Voters == null) throw new Exception("Voters is null");

            this.VotesFor = Voters?.Where(a => a.VoteFor == true)?.Count() ?? 0;
            this.VotesAgaints = Voters?.Where(a => a.VoteAgainst == true)?.Count() ?? 0;
        }
    }
}
