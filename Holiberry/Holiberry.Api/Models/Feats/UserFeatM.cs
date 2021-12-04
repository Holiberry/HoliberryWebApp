using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Feats
{
    public class UserFeatM : EntityBaseM
    {
        public DateTimeOffset CreatedAt { get; set; }

        public long UserId { get; set; }
        public UserM User { get; set; }

        public long FeatId { get; set; }
        public FeatM Feat { get; set; }
    }
}
