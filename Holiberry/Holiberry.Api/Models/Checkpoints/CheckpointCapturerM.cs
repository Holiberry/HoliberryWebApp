using Holiberry.Api.Models.Common.Entities;
using Holiberry.Api.Models.Users.Entities.Identity;
using System;

namespace Holiberry.Api.Models.Checkpoints
{
    public class CheckpointCapturerM : EntityBaseM
    {

        public DateTimeOffset CreatedAt { get; set; }

        public long UserId { get; set; }
        public UserM User { get; set; }

        public long CheckpointId { get; set; }
        public UserCheckpointM Checkpoint { get; set; }
    }
}