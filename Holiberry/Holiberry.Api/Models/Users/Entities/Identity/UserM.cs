using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Holiberry.Api.Models.Feats;
using Holiberry.Api.Models.Prizes;
using Holiberry.Api.Models.Quests;
using Holiberry.Api.Models.Schools;
using Holiberry.Api.Models.Stats;
using Holiberry.Api.Models.Threats;
using Microsoft.AspNetCore.Identity;


namespace Holiberry.Api.Models.Users.Entities.Identity
{

    public class UserM : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }


        public int Points { get; set; }






        public long? HomeId { get; set; }
        public UserHomeM Home { get; set; }


        public long? SchoolId { get; set; }
        public SchoolM School { get; set; }


        public long? OtherAddressId { get; set; }
        public UserAddressM OtherAddress { get; set; }

        public long? ParentId { get; set; }
        public UserM Parent { get; set; }




        // ---------------  statystyki ---------------  //
        public double TotalDistanceWalking { get; set; }
        public double TotalDistanceBike { get; set; }
        public double TotalDistanceScooter { get; set; }
        public double TotalPrizes { get; set; }
        public double TotalQuests { get; set; }
        public int TotalPointsEarned { get; set; }
        // --------------- /statystyki ---------------  //


        public virtual ICollection<UserRoleM> UserRoles { get; set; } = new HashSet<UserRoleM>();
        public virtual ICollection<UserClaimM> Claims { get; set; } = new HashSet<UserClaimM>();
        
        public virtual ICollection<UserPrizeM> Prizes { get; set; }
        public virtual ICollection<UserFeatM> Feats { get; set; }
        public virtual ICollection<UserDailyStatM> Stats { get; set; }
        public virtual ICollection<UserQuestM> Quests { get; set; }
        public virtual ICollection<UserThreatM> Threats { get; set; }



        public void Create(string email)
        {
            CreatedAt = DateTimeOffset.Now;

            UserName = email;
            NormalizedUserName = email.ToUpperInvariant();

            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
        }
    }
}
