using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Holiberry.Api.Models.Users.Entities.Identity
{
    
    public class UserRoleM : IdentityUserRole<long>
    {
        public virtual UserM User { get; set; }
        public virtual RoleM Role { get; set; }

    }
}
