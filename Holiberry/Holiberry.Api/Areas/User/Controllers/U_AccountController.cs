using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Extensions;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Models.Quests;
using Holiberry.Api.Models.Users.Entities.Identity;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.User.Controllers
{
    [Route("v1/user/account")]
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.User)]
    [ApiAuthorize]
    public class U_AccountController : Controller
    {
        private readonly UserManager<UserM> _userManager;
        private readonly SignInManager<UserM> _signInManager;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public U_AccountController(
            UserManager<UserM> userManager,
            SignInManager<UserM> signInManager,
            ApplicationDbContext db,
            IWebHostEnvironment env
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;

            _env = env;
        }


        


        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var _loggedUserId = User.GetUserId();

            var user = await _db.Users.AsNoTracking()
                .Where(a => a.Id == _loggedUserId)
                .Include(a => a.Claims)
                .Include(a => a.UserRoles)
                .Select(a => new UserM
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    Points = a.Points,
                    Home = a.Home,
                    School = a.School,
                    TotalDistanceBike = a.TotalDistanceBike,
                    TotalDistanceScooter = a.TotalDistanceScooter,
                    TotalDistanceWalking = a.TotalDistanceWalking,
                    TotalPrizes = a.TotalPrizes,
                    TotalQuests = a.TotalQuests,
                    TotalPointsEarned = a.TotalPointsEarned,

                })
                .FirstOrDefaultAsync() ?? throw new ServiceException("Nie znaleziono użytkownika");


            return Ok(new
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Login = user.Email,
                Roles = await _userManager.GetRolesAsync(user),

                Points = user.Points,
                Home = user.Home,
                School = user.School,
                TotalDistanceBike = user.TotalDistanceBike,
                TotalDistanceScooter = user.TotalDistanceScooter,
                TotalDistanceWalking = user.TotalDistanceWalking,
                TotalPrizes = user.TotalPrizes,
                TotalQuests = user.TotalQuests,
                TotalPointsEarned = user.TotalPointsEarned,
                
                hasSchool = user.School != null,
                hasHome = user.Home != null,
                hasParent = user.ParentId != null,
            });
        }



        [HttpGet("current-quests")]
        public async Task<IActionResult> GetCurrentQuests()
        {
            var quests = new List<QuestM>()
            {
                new QuestM()
                {
                    Description = "Pokonaj pieszo 2km!",
                    Name = "Dzień nóg!",
                    PrizePointsAmount = 10,
                    DateFrom = DateTimeOffset.Now.Date.AddDays(-1),
                    DateTo = DateTimeOffset.Now.Date.AddDays(1),
                    Status = QuestStatusE.Active
                },
                new QuestM()
                {
                    Description = "Pokonaj rowerem 5km!",
                    Name = "Wycieczkowo!",
                    PrizePointsAmount = 25,
                    DateFrom = DateTimeOffset.Now.Date.AddDays(-1),
                    DateTo = DateTimeOffset.Now.Date.AddDays(1),
                    Status = QuestStatusE.Active
                },
                new QuestM()
                {
                    Description = "Dostań się do szkoły hulajnogą!",
                    Name = "Happy scooter!",
                    PrizePointsAmount = 15,
                    DateFrom = DateTimeOffset.Now.Date.AddDays(-1),
                    DateTo = DateTimeOffset.Now.Date.AddDays(1),
                    Status = QuestStatusE.Active
                }
            };


            return Ok(quests);
        }


        [HttpGet("quests")]
        public async Task<IActionResult> GetMyCurrentQuests()
        {
            var quests = new List<UserQuestM>()
            {
                new UserQuestM()
                {
                    QuestId = 3,
                    Status = UserQuestStatusE.Active,
                    CreatedAt = DateTimeOffset.Now,
                },
                new UserQuestM()
                {
                    QuestId = 4,
                    Status = UserQuestStatusE.Finished,
                    CreatedAt = DateTimeOffset.Now,
                },
            };


            return Ok(quests);
        }
















        }
}
