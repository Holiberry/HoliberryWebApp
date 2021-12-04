using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Holiberry.Api.Models.Users.Entities.Identity;
using Holiberry.Api.Models.Users.Statics;

namespace Holiberry.Api.Helpers
{
    public static class SelectListHelpers
    {

        public static async Task<SelectList> GetRolesList(this IQueryable<RoleM> roles)
        {
            var rolesList = await roles.AsNoTracking()
                    .Select(n => new
                    {
                        Value = n.Name,
                        Text = n.Name
                    }).ToListAsync();
            return new SelectList(rolesList, "Value", "Text");
        }

        public static SelectList GetClaimsList()
        {
            var claimsList = UserClaims.ClaimsList
                .Select(a => new
                {
                    Value = a
                })
                .ToList();

            return new SelectList(claimsList, "Value", "Value");
        }

    }
}
