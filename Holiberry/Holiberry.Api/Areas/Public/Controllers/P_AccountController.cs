using Holiberry.Api.ActionResults;
using Holiberry.Api.Areas.Public.Requests.P_Account;
using Holiberry.Api.Areas.Public.Responses.P_Account;
using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Extensions;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Models.Users.Entities.Identity;
using Holiberry.Api.Models.Users.Statics;
using Holiberry.Api.Persistence;
using Holiberry.Api.ServerLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Public.Controllers
{

    [Authorize]
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.Public)]
    [Route("v1/public/account")]
    public class P_AccountController : Controller
    {

        private readonly UserManager<UserM> _userManager;
        private readonly SignInManager<UserM> _signInManager;
        private readonly RoleManager<RoleM> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IServerLogger _serverLogger;


        public P_AccountController(
            UserManager<UserM> userManager,
            SignInManager<UserM> signInManager,
            RoleManager<RoleM> roleManager,
            ApplicationDbContext db,
            IServerLogger serverLogger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            _serverLogger = serverLogger;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginAPIRequest req)
        {
            if (!ModelState.IsValid)
                return new ValidationResult();

            return Ok(await _login(req));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterAPIRequest req)
        {
            if (!ModelState.IsValid)
                return new ValidationResult();

            return Ok(await _register(req));
        }








        [AllowAnonymous]
        [HttpGet("create-roles")]
        public async Task<IActionResult> CreateRoles()
        {
            List<string> roles = new List<string>()
            {
                UserRoles.Admin,
                UserRoles.Dev,
            };

            var dbRoles = await _roleManager.Roles.AsNoTracking().ToListAsync();

            foreach (var role in roles)
            {
                if (!dbRoles.Any(a => a.Name == role))
                {
                    var roleM = new RoleM(role);
                    await _roleManager.CreateAsync(roleM);
                }
            }

            //ADMIN
            UserM u = new UserM
            {
                UserName = "admin@holiberry.com",
                FirstName = "Admin",
                LastName = "Holiberry",
                CreatedAt = DateTimeOffset.Now
            };


            u.Email = u.UserName;


            if (!await _userManager.Users.AsNoTracking().AnyAsync(a => a.UserName == u.UserName))
            {
                var userCreateResult = await _userManager.CreateAsync(u, "!23Haslo");
            }

            UserM user = _userManager.Users
                .SingleOrDefault(b => b.Email == u.Email);

            if (user != null)
            {
                //var result1 = _roleManager.Roles.ToList();
                var result = await _userManager.AddToRoleAsync(user, UserRoles.Admin);
                await _userManager.AddToRoleAsync(user, UserRoles.Dev);
            }

            return Json("roles were created");
        }







        private async Task<LoginAPIResponse> _login(LoginAPIRequest req)
        {
            var _loggedUser = User;

            //Jeśli użytkownik jest zalogowany
            if (_loggedUser.Identity.IsAuthenticated) throw new ServiceException().SingleError("USER_LOGGED_IN", "Użytkownik jest już zalogowany");


            var user = (await _userManager.FindByEmailAsync(req.Login)) ?? throw new ServiceException().SingleError("USER_LOGIN_FAIL", "Podałeś nieprawidłowe dane logowania. Spróbuj ponownie");


            //Sprwadzanie poprawności logowania
            var result = await _signInManager.CheckPasswordSignInAsync(user, req.Password, true);

            //Sprwadzanie czy użytkownik nie jest zablokowany (nowy system) -> pole UserM.LockoutEnabled zezwala tylko na zablokowanie konta, bez niego pomimo blokady logowanie przebiega pomyslnie
            if (result.IsLockedOut == true)
                throw new ServiceException("Logowanie na Twoje konto zostało zablokowane").AddError("USER_LOCKED_OUT", GetUserLockoutTimeLeftMessage(user.LockoutEnd.Value)).AddError("LOCKOUT_END:", $"Koniec blokady: {user.LockoutEnd?.ToLocalTimeString(format: "HH:mm dd-MM-yy")}");


            if (!result.Succeeded)
            {
                throw new ServiceException().SingleError("USER_LOGIN_FAIL", "Podałeś nieprawidłowe dane logowania. Spróbuj ponownie");
            }

            //Tworzenie nowego tokenu
            (string token, long tokenExpires, List<string> userRoles) = await GenerateAccessToken(user);



            return new LoginAPIResponse()
            {
                UserId = user.Id,
                Login = user.UserName,
                Token = token,
                Roles = userRoles,
                TokenExpires = tokenExpires
            };

            string GetUserLockoutTimeLeftMessage(DateTimeOffset date)
            {
                long minLeft;

                if (date < DateTimeOffset.Now)
                {
                    minLeft = 0;
                }
                else
                {
                    TimeSpan timeLeft = date - DateTimeOffset.Now;

                    minLeft = (long)Math.Ceiling(timeLeft.TotalMinutes);
                }

                return string.Format("Logowanie na Twoje konto zostało zablokowane z powodu wprowadzenia błędnego hasła - 5 razy. Spróbuj ponownie za {0} min lub skorzystaj z opcji \"Zapomniałem hasła\"", minLeft.ToString());
            }
        }

        private async Task<RegisterAPIResponse> _register(RegisterAPIRequest req)
        {
            //Tworzenie nowego użytkownika
            var user = new UserM();
            user.Create(req.Login);


            using (var transaction = await _db.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted))
            {
                var userCreateResult = await _userManager.CreateAsync(user, req.Password);
                if (!userCreateResult.Succeeded)
                {
                    throw new ServiceException().SingleError("USER_REGISTER_ERROR", "Nie udało się utworzyć konta");
                }


                await _db.SaveChangesAsync();

                transaction.Commit();
            }

            (string token, long tokenExpires, List<string> userRoles) = await GenerateAccessToken(user);



            return new RegisterAPIResponse()
            {
                UserId = user.Id,
                Login = user.UserName,
                Token = token,
                Roles = userRoles,
                TokenExpires = tokenExpires
            };
        }



        private async Task<(string token, long tokenExpires, List<string> userRoles)> GenerateAccessToken(UserM user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var claims = (await _userManager.GetClaimsAsync(user)).ToList();
            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            claims.AddRange(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim("TestUser", user.IsTest.ToString(), ClaimValueTypes.Boolean)
            });
            claims.AddRange(roles.Select(a => new Claim(ClaimTypes.Role, a)));

            if (user.Email != null)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            }
            else if (user.UserName != null)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.UserName));
            }
            else if (user.UserName != null)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.UserName));
            }


            var tokenExpires = DateTime.Now.AddDays(180);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigAuth.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var token = new JwtSecurityToken(ConfigAuth.ValidIssuer,
                   ConfigAuth.ValidAudience,
                   claims,
                   expires: tokenExpires,
                   signingCredentials: creds);

            return (
                token: handler.WriteToken(token),
                tokenExpires: new DateTimeOffset(tokenExpires).ToUnixTimeSeconds(),
                userRoles: roles
                );
        }
    }
}
