using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Public.Controllers
{
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.Public)]
    [ApiAuthorize]
    public class P_HomeController : ControllerBase
    {
        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok("ok");
        }
    }
}
