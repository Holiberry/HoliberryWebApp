using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.ViewModels.Common
{
    public class DeleteDataModalVM
    {
        public string ModalId { get; set; }
        public string Title { get; set; }
        public string QueryValue { get; set; }
        public string DeleteURL { get; set; }
        public bool CanBeDeleted { get; set; } = true;

        // Info why row can't be deleted
        public string CancelInfo { get; set; }
    }
}
