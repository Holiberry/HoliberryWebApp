using Holiberry.Api.Models.Common.Entities;

namespace Holiberry.Api.Models.Quests
{
    public class QuestConditionM : EntityBaseM
    {
        public int Amount { get; set; }
        public bool GeaterOrEqual { get; set; }
        public bool LessOrEqual { get; set; }


        public long QuestId { get; set; }
        public virtual QuestM Quest { get; set; }
    }
}