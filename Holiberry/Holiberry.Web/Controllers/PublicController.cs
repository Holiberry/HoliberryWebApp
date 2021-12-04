using Holiberry.Web.Attributes;
using Holiberry.Web.Config;
using Holiberry.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Web.Controllers
{

    [ViewLayout(LayoutsConfig.Public)]
    public class PublicController : Controller
    {
        private readonly ILogger<PublicController> _logger;

        public PublicController(ILogger<PublicController> logger)
        {
            _logger = logger;
        }





        public IActionResult Ranking()
        {
            return View();
        }


        public IActionResult ViewSchools()
        {
            var vm = new ViewSchoolsVM();

            return View(vm);
        }
    }
}
