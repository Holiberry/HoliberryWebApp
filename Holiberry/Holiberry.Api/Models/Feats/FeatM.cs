using Holiberry.Api.Models.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Feats
{
    public class FeatM : EntityBaseM
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public FeatTypeE Type { get; set; }
        
        public int PrizePointsAmount { get; set; }


        public long? PhotoId { get; set; }
        public FeatPhotoM Photo { get; set; }



        public long? ConditionId { get; set; }
        public FeatConditionM Condition { get; set; }



        public string GetPhotoUrl()
        {
            return string.Empty;
        }
    }
}
