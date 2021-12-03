using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Holiberry.Api.Models.Users.Entities.Identity
{
    public class UserClaimM : IdentityUserClaim<long>
    {
        public virtual UserM User { get; set; }
    }
}
