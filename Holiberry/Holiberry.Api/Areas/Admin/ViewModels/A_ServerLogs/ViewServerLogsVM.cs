using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Holiberry.Api.Common.Pagination;
using Holiberry.Api.Models.ServerLogs.Entities;
using Holiberry.Api.ViewModels.Pagination;

namespace Holiberry.Api.Areas.Admin.ViewModels.A_ServerLogs
{
    public class ViewServerLogsVM : PagerVM
    {
        public PagedResponse<ServerLogM> ServerLogs { get; set; }

        // Search params
        public string Search { get; set; }
    }
}
