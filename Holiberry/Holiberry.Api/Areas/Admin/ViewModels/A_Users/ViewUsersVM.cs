using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Holiberry.Api.Common.Pagination;
using Holiberry.Api.Models.Users.Entities.Identity;
using Holiberry.Api.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Admin.ViewModels.A_Users
{
    public class ViewUsersVM : PagerVM
    {
        public PagedResponse<UserM> Users { get; set; }

        // Search params
        public string Search { get; set; }
        public string Role { get; set; }
       
    }
}
