//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Holiberry.Api.Models.Users.Entities.Identity;
//using Holiberry.Api.Models.Users.Statics;
//using Holiberry.Api.Attributes;
//using Holiberry.Api.Config;
//using Holiberry.Api.Persistence;
//using System;
//using Holiberry.Api.Models.Exceptions;
//using Holiberry.Api.ServerLogs;
//using Holiberry.Api.Extensions;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using Holiberry.Api.Areas.Web.ViewModels.W_Account;

//namespace Holiberry.Api.Areas.Public.Controllers
//{
//    [Area(AreasConfig.Web)]
//    [ViewLayout(LayoutsConfig.Web)]
//    [TypeFilter(typeof(WebExceptionFilterAttribute))]
//    [Route("account")]
//    public class W_AccountController : Controller
//    {
//        private readonly UserManager<UserM> _userManager;
//        private readonly SignInManager<UserM> _signInManager;
//        private readonly RoleManager<RoleM> _roleManager;
//        private readonly ApplicationDbContext _db;
//        private readonly IServerLogger _serverLogger;


//        public W_AccountController(
//            UserManager<UserM> userManager,
//            SignInManager<UserM> signInManager,
//            RoleManager<RoleM> roleManager,
//            ApplicationDbContext db,
//            IServerLogger serverLogger)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
//            _db = db;
//            _serverLogger = serverLogger;
//        }

//        [AllowAnonymous]
//        [Route("login")]
//        public async Task<IActionResult> Login(string returnUrl = null)
//        {
//            ////Redirect if user is logged
//            if (_signInManager.IsSignedIn(User))
//                return RedirectToAction(nameof(RedirectAfterLogin));

//            // Clear the existing external cookie to ensure a clean login process
//            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

//            ViewData["ReturnUrl"] = returnUrl;

//            return View(new LoginVM());
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        [Route("login")]
//        public async Task<IActionResult> Login(LoginVM model, string returnUrl = null)
//        {
//            ////Redirect if user is logged
//            if (_signInManager.IsSignedIn(User))
//                return RedirectToAction(nameof(RedirectAfterLogin));

//            if (string.IsNullOrWhiteSpace(model?.Email))
//                ModelState.AddModelError("Email", "Pole nie może być puste");

//            if (string.IsNullOrWhiteSpace(model?.Password))
//                ModelState.AddModelError("Email", "Pole nie może być puste");

//            ViewData["ReturnUrl"] = returnUrl;
//            if (ModelState.IsValid)
//            {
//                var _user = await _db.Users
//                    .Where(a => a.UserName.ToUpper() == model.Email.ToUpper())
//                    .FirstOrDefaultAsync();

//                if (_user != null && _user.PasswordHash.Length == 32) // user istnieje i ma haslo w MD5
//                {
//                    MD5 md5 = MD5.Create();

//                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(model.Password);
//                    byte[] hash = md5.ComputeHash(inputBytes);

//                    StringBuilder sb = new StringBuilder();
//                    for (int i = 0; i < hash.Length; i++)
//                    {
//                        sb.Append(hash[i].ToString("x2"));
//                    }
                    
//                    var passwordHash = sb.ToString();


//                    if (_user.PasswordHash == passwordHash)
//                    {
//                        model.RememberMe = true;

//                        if (_user.SecurityStamp == null)
//                            _user.SecurityStamp = Guid.NewGuid().ToString();

//                        _user.PasswordHash = null;
//                        var response = await _userManager.AddPasswordAsync(_user, model.Password);
//                    }
//                }


//                model.RememberMe = true;
//                // This doesn't count login failures towards account lockout
//                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
//                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
//                if (result.Succeeded)
//                {

//                    //Check for change password
//                    //var user = _userManager.Users.AsNoTracking().SingleOrDefault(u => u.Email == model.Email);
//                    //if (user != null)
//                    //{
//                    //    //if (user.PasswordTemp != null)
//                    //    //    return RedirectToAction(nameof(ChangeTempPassword));
//                    //}
//                    return RedirectToLocal(returnUrl);
//                }
//                if (result.RequiresTwoFactor)
//                {
//                }
//                if (result.IsLockedOut)
//                {
//                }
//                else
//                {
//                    ModelState.AddModelError(string.Empty, "Niepoprawne dane logowania");
//                    return View(model);
//                }
//            }

//            // If we got this far, something failed, redisplay form
//            return View(model);
//        }

//        [Route("logout")]
//        public async Task<IActionResult> Logout()
//        {

//            await _signInManager.SignOutAsync();

//            //TODO: add logout view or not or delete comment :]
//            return RedirectToAction("Index", "W_Home");
//        }


//        [AllowAnonymous]
//        [HttpGet("forgot-password")]
//        public async Task<IActionResult> ForgotPassword(string email)
//        {
//            ////Redirect if user is logged
//            if (_signInManager.IsSignedIn(User))
//                return RedirectToAction(nameof(RedirectAfterLogin));

//            var vm = new ForgotPasswordVM()
//            {
//                Email = email
//            };

//            return View(vm);
//        }

//        [AllowAnonymous]
//        [HttpPost("forgot-password")]
//        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM vm)
//        {
//            ////Redirect if user is logged
//            if (_signInManager.IsSignedIn(User))
//                return RedirectToAction(nameof(RedirectAfterLogin));

//            if (!ModelState.IsValid)
//                return View(vm);

//            var user = await _db.Users
//                .FirstOrDefaultAsync(a => a.UserName.ToUpper() == vm.Email.ToUpper());

//            if (user != null)
//            {
//                if (string.IsNullOrWhiteSpace(user.SecurityStamp))
//                    user.SecurityStamp = Guid.NewGuid().ToString();

//                var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

//           }


//            return View(nameof(Login));
//        }

        
//        [AllowAnonymous]
//        [HttpGet("reset-password")] // NIE ZMIENIAĆ ROUTE ENDPOINTA BO WYSYPIE SIE EMAIL Z RESETEM HASLA
//        public async Task<IActionResult> ResetPassword(string email, string token)
//        {
//            ////Redirect if user is logged
//            if (_signInManager.IsSignedIn(User))
//                return RedirectToAction(nameof(RedirectAfterLogin));

//            if (string.IsNullOrWhiteSpace(email) || !await _db.Users.AsNoTracking().AnyAsync(a => a.UserName.ToUpper() == email.ToUpper()))
//                return NotFound();

//            if (string.IsNullOrWhiteSpace(token))
//                return NotFound();

//            var vm = new ResetPasswordVM()
//            {
//                Token = token,
//                Email = email
//            };

//            return View(vm);
//        }

//        [AllowAnonymous]
//        [HttpPost("reset-password")]
//        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
//        {
//            ////Redirect if user is logged
//            if (_signInManager.IsSignedIn(User))
//                return RedirectToAction(nameof(RedirectAfterLogin));

//            if (string.IsNullOrWhiteSpace(vm.Token) || string.IsNullOrWhiteSpace(vm.Email))
//                return NotFound();

//            var user = await _db.Users
//                .FirstOrDefaultAsync(a => a.UserName.ToUpper() == vm.Email.ToUpper());
            
//            if (user == null)
//                return NotFound();

//            if (!ModelState.IsValid)
//            {
//                return View(vm);            
//            }

//            if (string.IsNullOrWhiteSpace(user.SecurityStamp))
//                user.SecurityStamp = Guid.NewGuid().ToString();

//            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, vm.Token, vm.Password);
//            if (resetPasswordResult.Succeeded)
//            {

//                // po resecie hasla zaloguj automatycznie
//                var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, true, lockoutOnFailure: false);
//                if (result.Succeeded)
//                {
//                    return RedirectToAction(nameof(RedirectAfterLogin));
//                }

//                return View(nameof(Login));
//            }
//            else
//            {
//                throw new ServiceException("Nie udało się zresetować hasła");
//            }
//        }



//        //[AllowAnonymous]
//        //[HttpGet("register")]
//        //public async Task<IActionResult> Register()
//        //{
//        //    ////Redirect if user is logged
//        //    if (_signInManager.IsSignedIn(User))
//        //        return RedirectToAction(nameof(RedirectAfterLogin));

//        //    var vm = new RegisterVM()
//        //    {

//        //    };

//        //    return View(vm);
//        //}

//        //[AllowAnonymous]
//        //[HttpPost("register")]
//        //public async Task<IActionResult> Register(RegisterVM vm)
//        //{
//        //    if (_signInManager.IsSignedIn(User))
//        //        return RedirectToAction(nameof(RedirectAfterLogin));

//        //    if (await _db.Users.AsNoTracking().AnyAsync(a => a.Email.ToUpper() == vm.Email.ToUpper()))
//        //        ModelState.AddModelError("Email", "Podany adres email jest już zajęty");

//        //    if (!ModelState.IsValid)
//        //    {
//        //        return View(vm);
//        //    }


//        //    var user = new UserM()
//        //    {
//        //        CreatedAt = DateTime.Now,
//        //        FullName = string.Empty,
//        //        UserName = vm.Email,
//        //        Email = vm.Email,
//        //    };

//        //    var userCreateResult = await _userManager.CreateAsync(user, vm.Password);
//        //    if (!userCreateResult.Succeeded)
//        //    {
//        //        throw new ServiceException("Nie udało się stworzyć konta").LogException(userCreateResult);
//        //    }

//        //    try
//        //    {
//        //        var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

//        //        // email z kodem do potwierdzenia adresu email
//        //        await _emailService.SendConfirmEmailAddressEmail(user.Email, emailConfirmToken);
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        await _serverLogger.LogException(ex);
//        //    }

//        //    // zaloguj automatycznie po rejestracji konta
//        //    var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password, true, lockoutOnFailure: false);
//        //    if (result.Succeeded)
//        //    {
//        //        return RedirectToAction(nameof(RedirectAfterLogin));
//        //    }
                
//        //    return View(nameof(Login));
//        //}


//        [AllowAnonymous]
//        [HttpGet("confirm-email")] // NIE ZMIENIAĆ ROUTE ENDPOINTA BO WYSYPIE SIE EMAIL Z POTWIERDZENIEM EMAILA
//        public async Task<IActionResult> ConfirmEmail(string email, string token)
//        {
//            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
//                return NotFound();

//            var user = await _db.Users
//                .FirstOrDefaultAsync(a => a.Email.ToUpper() == email.ToUpper());
            
//            if (user == null)
//                return NotFound();

//            var confirmEmailResult = await _userManager.ConfirmEmailAsync(user, token);
//            if (confirmEmailResult.Succeeded)
//            {
//                return RedirectToAction(nameof(Login));
//            }
//            else
//            {
//                throw new ServiceException("Nie udało się potwierdzić adresu email");
//            }
//        }

//        public async Task<IActionResult> RedirectAfterLogin()
//        {
//            return await Task.FromResult(View());
//        }

//        private IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            else
//            {
//                return RedirectToAction(nameof(RedirectAfterLogin));
//            }
//        }

        



//        // utworzenie testowego usera 
//        //[AllowAnonymous]
//        //[Route("create-test-user")]
//        //public async Task<IActionResult> CreateTestUser()
//        //{

//        //    UserM u = new UserM();
//        //    u.UserName = "test@6moto.com";
//        //    u.Email = u.UserName;
//        //    u.FirstName = "TEST";
//        //    u.LastName = "TEST";
//        //    if (_userManager.Users.Count(a => a.UserName == u.UserName) == 0)
//        //    {
//        //        var userCreateResult = await _userManager.CreateAsync(u, "!23Haslo");
//        //    }

//        //    UserM user = _userManager.Users
//        //        .SingleOrDefault(b => b.Email == u.Email);
//        //    if (user != null)
//        //    {
//        //        var result1 = _roleManager.Roles.ToList();
//        //        var result = await _userManager.AddToRoleAsync(user, UserRoles.Dev);
//        //    }
//        //    return Json(new { Status = "Testowy user utworzony" });
//        //}



//        [AllowAnonymous]
//        [HttpGet("AccessDenied")]
//        public async Task<IActionResult> AccessDeniedView()
//        {
//            return View();
//        }
//    }
//}
