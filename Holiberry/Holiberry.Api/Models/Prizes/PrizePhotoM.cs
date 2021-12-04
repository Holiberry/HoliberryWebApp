using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Files.Entities;

namespace Holiberry.Api.Models.Prizes
{
    public class PrizePhotoM : EntityBaseM
    {
        public long FileId { get; set; }
        public DbFileM File { get; set; }

        public long PrizeId { get; set; }
        public PrizeM Prize { get; set; }
    }
}