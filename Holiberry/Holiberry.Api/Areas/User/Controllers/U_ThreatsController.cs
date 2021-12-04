using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Models.Threats;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.User.Controllers
{


    [Route("v1/user/threats")]
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.User)]
    [ApiAuthorize]
    public class U_ThreatsController : Controller
    {


        private readonly ApplicationDbContext _db;

        public U_ThreatsController(
            ApplicationDbContext db
            )
        {
            _db = db;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateThreat(double locLat, double locLng, UserThreatTypeE type)
        {


            return Ok(new
            {
                userThreatId = 5,
            });
        }


        [HttpPost("vote")]
        public async Task<IActionResult> VoteThreat(double locLat, double locLng, string voteType)
        {



            return Ok(new
            {
                userThreatId = 7,
            });
        }



    }
}
