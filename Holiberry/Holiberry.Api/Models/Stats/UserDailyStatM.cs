using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Stats
{
    public class UserDailyStatM : EntityBaseM
    {
        public StatTypeE Type { get; set; }
        public double Quantity { get; set; }

        
        public DateTime Date { get; set; }

        public long UserId { get; set; }
        public UserM User { get; set; }
    }
}
