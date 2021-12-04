using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Services.AirQuality;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Public.Controllers
{
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.Public)]
    [ApiAuthorize]
    public class P_HomeController : ControllerBase
    {
        private readonly IAirQualityApi _airQualityApi;


        public P_HomeController(IAirQualityApi airQualityApi)
        {
            _airQualityApi = airQualityApi;
        }




        //[Route("test")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Test()
        //{
        //    return Ok();
        //}
    }

    
}
