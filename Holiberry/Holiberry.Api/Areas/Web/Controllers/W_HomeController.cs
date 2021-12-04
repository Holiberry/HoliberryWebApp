//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Holiberry.Api.Attributes;
//using Holiberry.Api.Config;

//namespace Holiberry.Api.Areas.Public.Controllers
//{
//    [AllowAnonymous]
//    [Area(AreasConfig.Web)]
//    [ViewLayout(LayoutsConfig.Web)]
//    [TypeFilter(typeof(WebExceptionFilterAttribute))]
//    public class W_HomeController : Controller
//    {

//        public W_HomeController()
//        {

//        }

//        public IActionResult Index()
//        {
//            return RedirectToAction("Login", "W_Account", new { area = AreasConfig.Web });
//            //return View();
//        }

//    }
//}
