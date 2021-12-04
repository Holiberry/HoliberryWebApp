using Holiberry.Api.Models.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Prizes
{
    public class PrizeM : EntityBaseM
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public long? PhotoId { get; set; }
        public PrizePhotoM Photo { get; set; }
    }
}
