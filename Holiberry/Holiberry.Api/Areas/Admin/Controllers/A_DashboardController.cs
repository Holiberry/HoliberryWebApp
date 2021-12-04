//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Holiberry.Api.Models.Users.Statics;
//using Holiberry.Api.Persistence;
//using Holiberry.Api.Areas.Admin.ViewModels.A_Dashboard;
//using Holiberry.Api.Attributes;
//using Holiberry.Api.Config;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using Holiberry.Api.Models.Users.Entities.Identity;


//namespace Holiberry.Api.Areas.Admin.Controllers
//{

//    [Area(AreasConfig.Admin)]
//    [ViewLayout(LayoutsConfig.Admin)]
//    [AuthorizeRoles(UserRoles.Admin)]
//    [TypeFilter(typeof(WebExceptionFilterAttribute))]
//    public class A_DashboardController : Controller
//    {
//        private readonly ApplicationDbContext _db;

//        public A_DashboardController(
//            ApplicationDbContext db)
//        {
//            _db = db;
//        }

//        [Route("admin/dashboard")]
//        public async Task<IActionResult> ViewAdminDashboard(ViewAdminDashboardVM vm)
//        {
           
//            return View();
//        }



//        [Route("admin/test")]
//        public async Task<IActionResult> Test()
//        {
//            //await _emailService.SendOrderAcceptanceEmail(1, 26938);

//            //var users = await _db.Users.AsNoTracking()
//            //    .Where(a => a.WMSNumber.Length > 10)
//            //    .Select(a => a.WMSNumber)
//            //    .Distinct()
//            //    .ToListAsync();


//            //await _webJobsService.HandleProductStockUpdate();

//            //await _webJobsService.HandleProductShortExpirationStatus();


//            //var users = await _db.Users.AsNoTracking()
//            //    .Include(a => a.Claims)
//            //    .ToListAsync();

//            //var claimsToAdd = new List<UserClaimM>();
//            //var claimsToRemove = new List<UserClaimM>();
//            //foreach (var user in users)
//            //{
//            //    // claimy do usunięcia - wszystkie poza aktualnym comapny Id
//            //    var claims = user.Claims.Where(a => a.ClaimType == UserClaims.CompanyId && (user.CompanyId == null || user.CompanyId != null && a.ClaimValue != user.CompanyId.ToString())).ToList();
//            //    claimsToRemove.AddRange(claims);

//            //    if (user.CompanyId != null)
//            //    {
//            //        // jeśli nie ma już takiego claima to dodaj
//            //        if (!user.Claims.Any(a => a.ClaimType == UserClaims.CompanyId && a.ClaimValue == user.CompanyId.ToString()))
//            //        {
//            //            var claim = new UserClaimM()
//            //            {
//            //                UserId = user.Id,
//            //                ClaimType = UserClaims.CompanyId,
//            //                ClaimValue = user.CompanyId.ToString()
//            //            };

//            //            claimsToAdd.Add(claim);
//            //        }
//            //    }
//            //}

//            //if (claimsToRemove.Any())
//            //{
//            //    _db.RemoveRange(claimsToRemove);
//            //}
//            //if (claimsToAdd.Any())
//            //{
//            //    await _db.AddRangeAsync(claimsToAdd);
//            //}
//            //await _db.SaveChangesAsync();

//            //var products = await _db.Products
//            //    .ToListAsync();

//            //foreach (var product in products)
//            //{
//            //    product.AvisationStatus = Domain.Products.Enums.ProductAvisationStatusE.Completed;
//            //}

//            //await _db.SaveChangesAsync();

//            return Ok();
//        }
//    }
//}