using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Extensions;
using Holiberry.Api.Models.Tracks;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.User.Controllers
{
    [Route("v1/user/tracks")]
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.User)]
    [ApiAuthorize]
    public class U_TracksController : Controller
    {

        private readonly ApplicationDbContext _db;

        public U_TracksController(
            ApplicationDbContext db
            )
        {
            _db = db;
        }



        [HttpGet("my-tracks")]
        public async Task<IActionResult> GetMyTracks()
        {
            var _loggedUserId = User.GetUserId();

            //var user = await _db.Users.AsNoTracking()
            //    .Where(a => a.Id == _loggedUserId)
            //    .Include(a => a.Claims)
            //    .Include(a => a.UserRoles)
            //    .Select(a => new UserM
            //    {
            //        Id = a.Id,
            //        FirstName = a.FirstName,
            //        LastName = a.LastName,
            //        Email = a.Email
            //    })
            //    .FirstOrDefaultAsync() ?? throw new ServiceException("Nie znaleziono użytkownika");

            var tracks = new[]
            {
                new {
                    DestinationTypeStr = TrackDestinationTypeE.School.GetDisplayName(),
                    DestinationType = TrackDestinationTypeE.School,

                    TransportType = TrackTransportTypeE.Walking.GetDisplayName(),
                    TransportTypeStr = TrackTransportTypeE.Walking,

                    StatusStr = TrackStatusE.Finished.GetDisplayName(),
                    Status = TrackStatusE.Finished,
                    
                    LatStart = 51.10217606376395, 
                    LngStart = 17.10598562894213,

                    LatFinish = 51.10547298756479,
                    LngFinish = 17.094063345406525,

                    Distance = 0.895
                },
                new {
                    DestinationTypeStr = TrackDestinationTypeE.School.GetDisplayName(),
                    DestinationType = TrackDestinationTypeE.School,

                    TransportType = TrackTransportTypeE.Walking.GetDisplayName(),
                    TransportTypeStr = TrackTransportTypeE.Walking,

                    StatusStr = TrackStatusE.Finished.GetDisplayName(),
                    Status = TrackStatusE.Finished,
                    
                    LatStart = 51.10694780436874, 
                    LngStart = 17.11074419147315,

                    LatFinish = 51.10547298756479,
                    LngFinish = 17.094063345406525,

                    Distance = 1.17
                },
                new {
                    DestinationTypeStr = TrackDestinationTypeE.School.GetDisplayName(),
                    DestinationType = TrackDestinationTypeE.School,

                    TransportType = TrackTransportTypeE.Walking.GetDisplayName(),
                    TransportTypeStr = TrackTransportTypeE.Walking,

                    StatusStr = TrackStatusE.Finished.GetDisplayName(),
                    Status = TrackStatusE.Finished,
                    
                    LatStart = 51.10399416240677, 
                    LngStart = 17.11523199658681,

                    LatFinish = 51.10547298756479,
                    LngFinish = 17.094063345406525,

                    Distance = 1.426
                },
                new {
                    DestinationTypeStr = TrackDestinationTypeE.School.GetDisplayName(),
                    DestinationType = TrackDestinationTypeE.School,

                    TransportType = TrackTransportTypeE.Walking.GetDisplayName(),
                    TransportTypeStr = TrackTransportTypeE.Walking,

                    StatusStr = TrackStatusE.Finished.GetDisplayName(),
                    Status = TrackStatusE.Finished,
                    
                    LatStart = 51.10925626667652, 
                    LngStart = 17.096199377309585,

                    LatFinish = 51.10547298756479,
                    LngFinish = 17.094063345406525,

                    Distance = 0.457
                },
                new {
                    DestinationTypeStr = TrackDestinationTypeE.Home.GetDisplayName(),
                    DestinationType = TrackDestinationTypeE.Home,

                    TransportType = TrackTransportTypeE.Scooter.GetDisplayName(),
                    TransportTypeStr = TrackTransportTypeE.Scooter,

                    StatusStr = TrackStatusE.Finished.GetDisplayName(),
                    Status = TrackStatusE.Finished,
                    
                    LatStart = 51.10538613213304,  
                    LngStart = 17.094036579664444,

                    LatFinish = 51.10056498499633, 
                    LngFinish = 17.118043633525495,

                    Distance = 1.768
                },
                new {
                    DestinationTypeStr = TrackDestinationTypeE.Home.GetDisplayName(),
                    DestinationType = TrackDestinationTypeE.Home,

                    TransportType = TrackTransportTypeE.Scooter.GetDisplayName(),
                    TransportTypeStr = TrackTransportTypeE.Scooter,

                    StatusStr = TrackStatusE.Finished.GetDisplayName(),
                    Status = TrackStatusE.Finished,
                    
                    LatStart = 51.10664226395264,   
                    LngStart = 17.10387730894983,

                    LatFinish = 51.10056498499633, 
                    LngFinish = 17.118043633525495,

                    Distance = 1.268
                },
                new {
                    DestinationTypeStr = TrackDestinationTypeE.Home.GetDisplayName(),
                    DestinationType = TrackDestinationTypeE.Home,

                    TransportType = TrackTransportTypeE.Bike.GetDisplayName(),
                    TransportTypeStr = TrackTransportTypeE.Bike,

                    StatusStr = TrackStatusE.Finished.GetDisplayName(),
                    Status = TrackStatusE.Finished,
                    
                    LatStart = 51.10001949161073,   
                    LngStart = 17.091288101784013,

                    LatFinish = 51.10056498499633, 
                    LngFinish = 17.118043633525495,

                    Distance = 1.852
                },
                new {
                    DestinationTypeStr = TrackDestinationTypeE.Different.GetDisplayName(),
                    DestinationType = TrackDestinationTypeE.Different,

                    TransportType = TrackTransportTypeE.Bike.GetDisplayName(),
                    TransportTypeStr = TrackTransportTypeE.Bike,

                    StatusStr = TrackStatusE.Finished.GetDisplayName(),
                    Status = TrackStatusE.Finished,
                    
                    LatStart = 51.10056498499633,
                    LngStart = 17.118043633525495,
                                        
                    LatFinish = 51.10001949161073,
                    LngFinish = 17.091288101784013,

                    Distance = 2.175
                }
            };


            return Ok(tracks);
        }


        [HttpGet("my-tracks/track")]
        public async Task<IActionResult> GetMyTrack(long trackId)
        {
            return Ok(new
            {
                DestinationTypeStr = TrackDestinationTypeE.School.GetDisplayName(),
                DestinationType = TrackDestinationTypeE.School,

                TransportType = TrackTransportTypeE.Walking.GetDisplayName(),
                TransportTypeStr = TrackTransportTypeE.Walking,

                StatusStr = TrackStatusE.Finished.GetDisplayName(),
                Status = TrackStatusE.Finished,

                LatStart = 51.10217606376395,
                LngStart = 17.10598562894213,

                LatFinish = 51.10547298756479,
                LngFinish = 17.094063345406525,

                Distance = 0.895
            });
        }
        


        [HttpPost("my-tracks/start-track")]
        public async Task<IActionResult> StartTrack(string destinationType, string transportType, double locLat, double locLng)
        {
            

            return Ok(new 
            { 
                trackId = 1 
            });
        }



        [HttpPost("my-tracks/finish-track")]
        public async Task<IActionResult> FinishTrack(double locLat, double locLng)
        {
            



            return Ok(new 
            { 
                trackId = 1 
            });
        }


        [HttpGet("my-tracks/current-track")]
        public async Task<IActionResult> GetCurrentTrack()
        {
            

            return Ok(new
            {
                TrackId = 18,
                DestinationTypeStr = TrackDestinationTypeE.School.GetDisplayName(),
                DestinationType = TrackDestinationTypeE.School,

                TransportType = TrackTransportTypeE.Walking.GetDisplayName(),
                TransportTypeStr = TrackTransportTypeE.Walking,

                StatusStr = TrackStatusE.Active.GetDisplayName(),
                Status = TrackStatusE.Active,

                LatStart = 51.10217606376395,
                LngStart = 17.10598562894213,

                Distance = 0.154
            });
        }


    }
}
