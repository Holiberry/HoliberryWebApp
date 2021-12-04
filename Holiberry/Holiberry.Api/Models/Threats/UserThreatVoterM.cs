using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;

namespace Holiberry.Api.Models.Threats
{
    public class UserThreatVoterM : EntityBaseM
    {
        public bool VoteFor { get; set; }
        public bool VoteAgainst { get; set; }


        public long UserId { get; set; }
        public UserM User { get; set; }
    }
}