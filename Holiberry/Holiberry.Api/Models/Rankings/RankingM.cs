using Holiberry.Api.Models.Cities;
using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Schools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Rankings
{
    public class RankingM : EntityBaseM
    {
        public RankingTypeE Type { get; set; }
        

        public long? SchoolId { get; set; }
        public SchoolM School { get; set; }
        

        public long? CityId { get; set; }
        public CityM City { get; set; }
    }
}
