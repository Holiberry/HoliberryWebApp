using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Prizes
{
    public class UserPrizeM : EntityBaseM
    {
        public DateTimeOffset CreatedAt { get; set; }

        public bool IsUsed { get; set; }


        public long PrizeId { get; set; }
        public PrizeM Prize { get; set; }

        public long UserId { get; set; }
        public UserM User { get; set; }
    }
}
