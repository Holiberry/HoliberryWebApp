using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.ViewModels.Common
{
    public class DeleteRelationDataModalVM : DeleteDataModalVM
    {
        public long EntityId { get; set; }
        public string EntityName { get; set; }
        public string RelatedEntityName { get; set; }
        public string BtnDeleteClass { get; set; }
    }
}
