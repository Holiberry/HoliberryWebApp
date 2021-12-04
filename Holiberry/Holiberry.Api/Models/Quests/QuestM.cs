using Holiberry.Api.Models.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Models.Quests
{
    public class QuestM : EntityBaseM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrizePointsAmount { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public QuestStatusE Status { get; set; }
        public QuestTypeE Type { get; set; }


        public virtual ICollection<QuestConditionM> Contitions { get; set; }
    }
}
