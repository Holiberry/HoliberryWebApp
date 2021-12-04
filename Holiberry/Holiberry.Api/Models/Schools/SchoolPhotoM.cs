using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Files.Entities;

namespace Holiberry.Api.Models.Schools
{
    public class SchoolPhotoM : EntityBaseM
    {
        public long FileId { get; set; }
        public DbFileM File { get; set; }
        
        public long SchoolId { get; set; }
        public SchoolM School { get; set; }
    }
}