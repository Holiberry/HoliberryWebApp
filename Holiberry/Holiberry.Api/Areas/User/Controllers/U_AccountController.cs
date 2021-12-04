using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Extensions;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Models.Users.Entities.Identity;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    Email = a.Email
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
            });
        }



















        

    }
}
