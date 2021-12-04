using Holiberry.Api.Models.Common.Entities;

namespace Holiberry.Api.Models.Feats
{
    public class FeatConditionM : EntityBaseM
    {
        public int Amount { get; set; }
        public bool GreaterOrEqualThan { get; set; }
        public bool LessOrEqualThan { get; set; }



        public virtual FeatM Feat { get; set; }
    }
}