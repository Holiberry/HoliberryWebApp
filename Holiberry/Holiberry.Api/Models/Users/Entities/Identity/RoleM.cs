using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Holiberry.Api.Models.Users.Entities.Identity
{
    public class RoleM : IdentityRole<long>
    {
        public RoleM()
            : base()
        {
        }

        public RoleM(string roleName)
            : base(roleName)
        {
        }
        

        public virtual ICollection<UserRoleM> UserRoles { get; } = new HashSet<UserRoleM>();

    }
}
