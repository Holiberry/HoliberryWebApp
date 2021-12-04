using Holiberry.Api.Models.Users.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Admin.ViewModels.A_Users
{
    public class ViewUserVM
    {
        public int UserId { get; set; }

        public UserM User { get; set; }
    }
}
