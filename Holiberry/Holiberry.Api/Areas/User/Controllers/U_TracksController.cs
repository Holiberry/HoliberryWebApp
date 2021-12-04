using Holiberry.Api.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.User.Controllers
{
    [Route("v1/user/account")]
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [ApiAuthorize]
    public class U_TracksController : Controller
    {

    }
}
