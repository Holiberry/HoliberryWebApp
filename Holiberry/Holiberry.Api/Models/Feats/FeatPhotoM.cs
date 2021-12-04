using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Files.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Feats
{
    public class FeatPhotoM : EntityBaseM
    {
        public long FileId { get; set; }
        public DbFileM File { get; set; }

        public long FeatId { get; set; }
        public FeatM Feat { get; set; }
    }
}
