using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Holiberry.Api.Models.Users.Entities.Identity
{
    public class UserLoginM : IdentityUserLogin<long>
    {
        
    }
}
