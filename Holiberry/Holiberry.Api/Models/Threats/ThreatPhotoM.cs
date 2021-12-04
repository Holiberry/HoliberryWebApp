using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Files.Entities;

namespace Holiberry.Api.Models.Threats
{
    public class ThreatPhotoM : EntityBaseM
    {
        public long FileId { get; set; }
        public DbFileM File { get; set; }

        public long ThreatId { get; set; }
        public ThreatM Threat { get; set; }
    }
}