using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Extensions;
using Holiberry.Api.Models.Threats;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.User.Controllers
{


    [Route("v1/public/threats")]
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.User)]
    [ApiAuthorize]
    public class P_ThreatsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public P_ThreatsController(ApplicationDbContext db)
        {
            _db = db;
        }
        


        [HttpGet("")]
        public async Task<IActionResult> GetUserThreats()
        {
            var threats = new List<UserThreatM>()
            {
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.103466638727376, Lng = 17.01792967705635,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.10001726466975, Lng = 17.04711211034917,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.09376461817279, Lng = 17.041103962318292,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.118863689286535, Lng = 17.05383955985374,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.09978746941376, Lng = 17.000281211692812,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.09826156998582, Lng = 17.052683321625743,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.10289467965688, Lng = 17.099356062869838,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.1119157452424, Lng = 17.100934922657927,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.113781368267766, Lng = 17.089098555486476,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.10739841780485, Lng = 17.094357934220792,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.09976293629093, Lng = 17.105242117945547,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                },
                new UserThreatM()
                {
                    Id = 1,
                    VotesFor = 3,
                    Lat = 51.10256876117516, Lng = 17.08765486837109,
                    Type = UserThreatTypeE.Accident,
                    ExpirationDate = DateTimeOffset.Now.Date.AddDays(5)
                }
            };



            var userThreats = threats.Select(a => new
                {
                    Position = a.PositionDTO,
                    a.Lng,
                    a.VotesFor,
                    a.Type,
                    TypeStr = a.Type.GetDisplayName(),
                    PhotoUrl = a.GetPhotoUrl()
                })
                .ToList();

            return Ok(userThreats);
        }

    }
}
