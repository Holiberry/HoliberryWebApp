//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Holiberry.Api.Common.Pagination;
//using Holiberry.Api.Extensions;
//using Holiberry.Api.Models.Exceptions;
//using Holiberry.Api.Models.Users.Entities.Identity;
//using Holiberry.Api.Models.Users.Statics;
//using Holiberry.Api.Persistence;
//using Holiberry.Api.Areas.Admin.ViewModels.A_Users;
//using Holiberry.Api.Attributes;
//using Holiberry.Api.Config;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Newtonsoft.Json;
//using Holiberry.Api.Models.Users.Entities;
//using Holiberry.Api.Helpers;

//namespace Holiberry.Api.Areas.Admin.Controllers
//{
//    [Route("admin/users")]
//    [Area(AreasConfig.Admin)]
//    [ViewLayout(LayoutsConfig.Admin)]
//    [AuthorizeRoles(UserRoles.Admin)]
//    [TypeFilter(typeof(WebExceptionFilterAttribute))]
//    public class A_UsersController : Controller
//    {
//        private readonly ApplicationDbContext _db;
//        private readonly UserManager<UserM> _userManager;
//        private readonly RoleManager<RoleM> _roleManager;
//        private readonly IMapper _mapper;

//        public A_UsersController(
//            ApplicationDbContext db, 
//            UserManager<UserM> userManager, 
//            RoleManager<RoleM> roleManager,
//            IMapper mapper
//            )
//        {
//            _db = db;
//            _mapper = mapper;
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }

//        [HttpGet]
//        public async Task<IActionResult> ViewUsers(ViewUsersVM vm)
//        {
//            var query = _db.Users.AsNoTracking();

//            if (!string.IsNullOrWhiteSpace(vm.Search))
//            {
//                query = query.Where(a => a.UserName.Contains(vm.Search)
//                    || a.Email.Contains(vm.Search)
//                    || a.Id.ToString() == vm.Search
//                    || a.PhoneNumber.Contains(vm.Search)
//                    );
//            }
//            if (!string.IsNullOrWhiteSpace(vm.Role))
//            {
//                var role = await _db.Roles.AsNoTracking().FirstOrDefaultAsync(a => a.NormalizedName == vm.Role.ToUpper());
//                query = query.Where(a => a.UserRoles.Any(a => a.RoleId == role.Id));
//            }
            
//            query = query
//                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role);

//            vm.Users = await ServicePagedResponse.GetFilteredPagedResponse(query, vm.PaginationFilter);

//            return View(vm);
//        }

//        [Route("user")]
//        public async Task<IActionResult> ViewUser(ViewUserVM vm)
//        {
//            vm.User = await _db.Users.AsNoTracking()
//                .Where(a => a.Id == vm.UserId)
//                .Include(a => a.UserRoles).ThenInclude(a => a.Role)
//                .Include(a => a.Claims)
//                .FirstOrDefaultAsync() ?? throw new ServiceException("Nie znaleziono użytkownika");

//            return View(vm);
//        }

//        [Route("create")]
//        public IActionResult CreateUser()
//        {
//            var vm = new CreateUserVM();

//            return View(vm);
//        }

//        [HttpPost]
//        [Route("create")]
//        public async Task<IActionResult> CreateUser(CreateUserVM vm)
//        {
//            if (vm.Email != null && await _db.Users.AsNoTracking().AnyAsync(a => a.Email == vm.Email.ToUpper()))
//                ModelState.AddModelError("Email", "Email is already taken");

//            if (!ModelState.IsValid)
//                return View(nameof(CreateUser), vm);

//            var user = _mapper.Map<UserM>(vm);

//            user.CreatedAt = DateTimeOffset.Now;

//            var result = await _userManager.CreateAsync(user, vm.Password);

//            if (!result.Succeeded)
//            {
//                var exception = new ServiceException("Unable to create new user");
//                foreach (var error in result.Errors)
//                {
//                    exception.AddError(error.Code, error.Description);
//                }
//                throw exception;
//            }

//            return RedirectToAction(nameof(ViewUsers));
//        }

//        [Route("user/update")]
//        public async Task<IActionResult> UpdateUser(int userId)
//        {
//            var vm = await _db.Users.AsNoTracking()
//                .Where(a => a.Id == userId)
//                .ProjectTo<UpdateUserVM>(_mapper.ConfigurationProvider)
//                .FirstOrDefaultAsync() ?? throw new ServiceException("Nie znaleziono użytkownika");

//            return View(vm);
//        }

//        [HttpPost]
//        [Route("user/update")]
//        public async Task<IActionResult> UpdateUser(UpdateUserVM vm, int userId)
//        {
//            if (vm.Email != null && await _db.Users.AsNoTracking().AnyAsync(a => a.Id != userId && a.UserName.ToUpper() == vm.Email.ToUpper())) // vm.Email == null sprawdzane przez FluentValidatora
//                ModelState.AddModelError("Email", "Istnieje już konto o podanym adresie email");

//            if (!ModelState.IsValid)
//            {
//                return View(vm);
//            }

//            var user = await _db.Users
//                .FirstOrDefaultAsync(a => a.Id == userId) ?? throw new ServiceException("Nie znaleziono użytkownika");

//            _mapper.Map(vm, user);

//            await _db.SaveChangesAsync();

//            return RedirectToAction(nameof(ViewUser), new { userId });
//        }


//        [HttpPost]
//        [Route("user/add-role")]
//        public async Task<IActionResult> AddRoleToUser(int userId, string roleName)
//        {
//            var user = await _db.Users
//                .FirstOrDefaultAsync(u => u.Id == userId) ?? throw new ServiceException("Nie znaleziono użytkownika");

//            if (!await _roleManager.RoleExistsAsync(roleName))
//                throw new ServiceException("Rola nie istnieje");

//            if (await _userManager.IsInRoleAsync(user, roleName))
//                throw new ServiceException($"Użytkownik ma już przypisaną rolę: { roleName }");

//            var result = await _userManager.AddToRoleAsync(user, roleName);

//            if (!result.Succeeded)
//                throw new ServiceException($"Błąd przyporządkowania użytkownika do roli: {roleName}");

//            return RedirectToAction(nameof(ViewUser), new { userId });
//        }

//        [HttpPost]
//        [Route("user/remove-role")]
//        public async Task<IActionResult> RemoveRoleFromUser(int userId, string roleName)
//        {
//            UserM user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId) ?? throw new ServiceException("Nie znaleziono użytkownika");

//            if (!await _roleManager.RoleExistsAsync(roleName))
//                throw new ServiceException("Rola nie istnieje");

//            if (!await _userManager.IsInRoleAsync(user, roleName))
//                throw new ServiceException(string.Format("Użytkownik nie ma przypisanej roli {0}", roleName));

//            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

//            if (!result.Succeeded)
//                throw new ServiceException($"Błąd podczas usuwania roli użytkownika: {roleName}");

//            return RedirectToAction(nameof(ViewUser), new { userId });
//        }

//        [HttpPost]
//        [Route("user/add-claim")]
//        public async Task<IActionResult> AddClaimToUser(int userId, string claimName, string claimValue)
//        {
//            if (string.IsNullOrWhiteSpace(claimName) || string.IsNullOrWhiteSpace(claimValue) || !UserClaims.ClaimsList.Contains(claimName))
//                throw new ServiceException("Niepoprawna nazwa lub wartość claima");

//            UserM user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId) ?? throw new ServiceException("Nie znaleziono użytkownika");

//            var claims = await _userManager.GetClaimsAsync(user);

//            if (!claims.Any(c => c.Type == claimName && c.Value == claimValue))
//            {
//                switch (claimName)
//                {

//                    default:
//                        break;
//                }

//                if(user.SecurityStamp == null)
//                {
//                    user.SecurityStamp = Guid.NewGuid().ToString();
//                    _db.Entry(user).Property(a => a.SecurityStamp).IsModified = true;
//                    await _db.SaveChangesAsync();
//                }

//                _db.UserClaims.Add(new UserClaimM() { ClaimType = claimName, ClaimValue = claimValue, UserId = userId });
//                await _db.SaveChangesAsync();

//                // TODO: fix bo nie działa
//                //var claimResult = await _userManager.AddClaimAsync(user, new Claim(claimName, claimValue));

//                //if (!claimResult.Succeeded)
//                //    throw new ServiceException(string.Format("Nie udało się utworzyć claima:", string.Join(", ", claimResult.Errors.Select(a => $"{a.Code} - {a.Description}"))));
//            }
//            else
//                throw new ServiceException("Do użytkownika jest już przypisany taki claim");

//            return RedirectToAction(nameof(ViewUser), new { userId });
//        }


//        [HttpPost]
//        [Route("user/remove-claim")]
//        public async Task<IActionResult> RemoveClaimFromUser(int userId, string claimName)
//        {
//            UserM user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId) ?? throw new ServiceException("Nie znaleziono użytkownika");

//            var userClaims = await _userManager.GetClaimsAsync(user);

//            Claim claimToRemove = userClaims.FirstOrDefault(c => c.Type == claimName) ?? throw new ServiceException(string.Format("Użytkownik nie posiada claima: {0}", claimName));

//            var result = await _userManager.RemoveClaimAsync(user, claimToRemove);

//            if (!result.Succeeded)
//                throw new ServiceException("Wystapił błąd podczas usuwania claimu");

//            return RedirectToAction(nameof(ViewUser), new { userId });
//        }


//        [HttpPost("user/change-password")]
//        public async Task<IActionResult> UserChangePassword(int userId, string password, string confirmPassword)
//        {
//            if (string.IsNullOrWhiteSpace(password))
//                throw new ServiceException("Hasło nie może być puste");

//            if (password != confirmPassword)
//                throw new ServiceException("Hasła nie są takie same");

//            var user = await _db.Users
//                .FirstOrDefaultAsync(a => a.Id == userId) ?? throw new ServiceException("Nie znaleziono użytkownika").AddError("Id", userId.ToString());

//            user.PasswordHash = null;

//            var addNewPasswordResult = await _userManager.AddPasswordAsync(user, password);
//            if (!addNewPasswordResult.Succeeded)
//            {
//                throw new ServiceException("Wystąpił błąd podczas zmiany hasła użytkownika")
//                    .AddError("Id", userId.ToString())
//                    .AddRangeErrors(addNewPasswordResult.Errors?.ToDictionary(a => a.Code, a => a.Description));
//            }

//            return RedirectToAction(nameof(ViewUser), new { userId });
//        }

//        //[HttpPost("user/send-user-email-confirm-email")]
//        //public async Task<IActionResult> SendUserEmailConfirmEmail(int userId)
//        //{
//        //    var user = await _db.Users.AsNoTracking()
//        //        .Where(a => a.Id == userId)
//        //        .FirstOrDefaultAsync() ?? throw new ServiceException("Nie znaleziono użytkownika").AddError("Id", userId.ToString());

//        //    var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

//        //    await _emailService.SendConfirmEmailAddressEmail(user.Email, confirmEmailToken);

//        //    return RedirectToAction(nameof(ViewUser), new { userId });
//        //}

//        //[HttpPost("user/send-user-password-reset-email")]
//        //public async Task<IActionResult> SendUserResetPasswordEmail(int userId)
//        //{
//        //    var user = await _db.Users.AsNoTracking()
//        //        .Where(a => a.Id == userId)
//        //        .FirstOrDefaultAsync() ?? throw new ServiceException("Nie znaleziono użytkownika").AddError("Id", userId.ToString());

//        //    var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

//        //    await _emailService.SendResetPasswordEmail(user.Id, passwordResetToken);

//        //    return RedirectToAction(nameof(ViewUser), new { userId });
//        //}

      
//        [ViewLayout(LayoutsConfig.Clear)]
//        [Route("user/info-partial")]
//        public async Task<IActionResult> ViewUserInfoPartial(int userId)
//        {
//            var vm = new ViewUserInfoPartialVM()
//            {
//                UserId = userId
//            };

//            vm.User = await _db.Users.AsNoTracking()
//                .Where(a => a.Id == vm.UserId)
//                .Include(a => a.UserRoles).ThenInclude(a => a.Role)
//                .Include(a => a.Claims)
//                .FirstOrDefaultAsync() ?? throw new ServiceException("Nie znaleziono użytkownika");

//            ViewData["Roles"] = await _db.Roles.GetRolesList();
//            ViewData["Claims"] = SelectListHelpers.GetClaimsList();

//            return View(vm);
//        }

//    }
//}