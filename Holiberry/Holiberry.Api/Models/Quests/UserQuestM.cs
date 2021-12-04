using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Quests
{
    public class UserQuestM : EntityBaseM
    {
        public DateTimeOffset CreatedAt { get; set; }

        public UserQuestStatusE Status { get; set; }

        
        public long UserId { get; set; }
        public UserM User { get; set; }

        public long QuestId { get; set; }
        public QuestM Quest { get; set; }
    }
}
