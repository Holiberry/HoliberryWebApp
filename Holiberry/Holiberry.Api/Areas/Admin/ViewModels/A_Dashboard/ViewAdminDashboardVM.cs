using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Admin.ViewModels.A_Dashboard
{
    public class ViewAdminDashboardVM
    {
        public int EventsCount { get; set; }
        public int EventsDeletedCount { get; set; }


        public int UserLicensesCount { get; set; }
        public Dictionary<string, int> UserLicensesCountByType { get; set; }


        public int UsersCount { get; set; }
        public int UsersProCount { get; set; }
        public int UsersLockedCount { get; set; }


        public int OrdersCount { get; set; }
        public int OrdersNewCount { get; set; }
        public int OrdersCompletedCount { get; set; }
        public decimal OrdersCompletedTotalCount { get; set; }
    }
}
