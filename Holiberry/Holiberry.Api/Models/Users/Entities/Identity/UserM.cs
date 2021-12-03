using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Identity;


namespace Holiberry.Api.Models.Users.Entities.Identity
{
    
    public class UserM : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }



        public virtual ICollection<UserRoleM> UserRoles { get; set; } = new HashSet<UserRoleM>();
        public virtual ICollection<UserClaimM> Claims { get; set; } = new HashSet<UserClaimM>();



        public void Create(string email)
        {
            CreatedAt = DateTimeOffset.Now;

            UserName = email;
            NormalizedUserName = email.ToUpperInvariant();

            Email = email;
            NormalizedEmail = email.ToUpperInvariant();
        }
    }
}
