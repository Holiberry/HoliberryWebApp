using Holiberry.Api.Common.DTO;
using Holiberry.Api.Config;
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
        public PositionDTO PositionDTO => Lat != null && Lng != null ? new PositionDTO(Lng.Value, Lat.Value) : null;


        public DateTimeOffset ExpirationDate { get; set; }

        
        public int VotesFor { get; set; }
        public int VotesAgaints { get; set; }

        public virtual ICollection<UserThreatVoterM> Voters { get; set; }


        public string GetPhotoUrl() 
        {
            return this.Type switch
            {
                UserThreatTypeE.Danger => $"{ConfigAPI.WebAppUrl}images/zagrozenia.png",
                UserThreatTypeE.NoSideWalk => $"{ConfigAPI.WebAppUrl}images/brakchodnika.png",
                UserThreatTypeE.Noise => $"{ConfigAPI.WebAppUrl}images/brakchodnika.png",
                UserThreatTypeE.DangerousPlace => $"{ConfigAPI.WebAppUrl}images/niebezpiecznemiejsca.png",
                UserThreatTypeE.DangerousPass => $"{ConfigAPI.WebAppUrl}images/niebezpieczneprzejscie.png",
                UserThreatTypeE.NoLights => $"{ConfigAPI.WebAppUrl}images/nieoswietlonadroga.png",
                UserThreatTypeE.UnevenSidewalk => $"{ConfigAPI.WebAppUrl}images/nierownychodnik.png",
                UserThreatTypeE.RoadWorks => $"{ConfigAPI.WebAppUrl}images/robotydrogowe.png",
                UserThreatTypeE.Accident => $"{ConfigAPI.WebAppUrl}images/wypadek.png",
                _ => string.Empty
            };
        }

        public void RefreshVotes() 
        {
            if (Voters == null) throw new Exception("Voters is null");

            this.VotesFor = Voters?.Where(a => a.VoteFor == true)?.Count() ?? 0;
            this.VotesAgaints = Voters?.Where(a => a.VoteAgainst == true)?.Count() ?? 0;
        }
    }
}
