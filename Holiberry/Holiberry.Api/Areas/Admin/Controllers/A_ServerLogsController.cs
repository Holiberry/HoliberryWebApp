//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Holiberry.Api.Config;
//using Holiberry.Api.Attributes;
//using Holiberry.Api.Models.Users.Statics;
//using Holiberry.Api.Persistence;
//using Holiberry.Api.Areas.Admin.ViewModels.A_ServerLogs;
//using Holiberry.Api.Common.Pagination;

//namespace FMLogistic.WebApp.Areas.Admin.Controllers
//{
//    [Route("admin/server-logs")]
//    [Area(AreasConfig.Admin)]
//    [ViewLayout(LayoutsConfig.Admin)]
//    [AuthorizeRoles(UserRoles.Admin)]
//    [TypeFilter(typeof(WebExceptionFilterAttribute))]
//    public class A_ServerLogsController : Controller
//    {
//        private readonly ApplicationDbContext _db;

//        public A_ServerLogsController(
//            ApplicationDbContext db
//            )
//        {
//            _db = db;
//        }


//        public async Task<IActionResult> ViewServerLogs(ViewServerLogsVM vm)
//        {
//            var query = _db.ServerLogs.AsNoTracking();


//            if (!string.IsNullOrWhiteSpace(vm.Search))
//            {
//                query = query.Where(a => a.Message.Contains(vm.Search));
//            }

//            vm.ServerLogs = await ServicePagedResponse.GetFilteredPagedResponse(query, vm.PaginationFilter);

//            return View(vm);
//        }
//    }
//}
