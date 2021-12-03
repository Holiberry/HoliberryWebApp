using System;
using System.Security.Claims;

namespace Holiberry.Api.Extensions
{
    public static class IdentityExtensions
    {

        public static int GetUserId(this ClaimsPrincipal user)
        {

            int result = user.TryGetUserId();
            if (result == 0)
                throw new UnauthorizedAccessException("Access denied").AddError("Id", "");

            return result;
        }


        public static int TryGetUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return 0;

            var userIdString = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            int.TryParse(userIdString, out int result);

            return result;
        }


        public static string GetUserUserName(this ClaimsPrincipal user)
        {
            try
            {
                if (!user.Identity.IsAuthenticated)
                    return "";

                ClaimsPrincipal currentUser = user;
                var userName = currentUser.FindFirst(ClaimTypes.Name).Value;
                return userName;
            }
            catch
            {
                return "";
            }
        }
    }
}
