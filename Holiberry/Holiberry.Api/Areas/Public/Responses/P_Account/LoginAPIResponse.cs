using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Areas.Public.Responses.P_Account
{
    public class LoginAPIResponse
    {
        public string Login { get; set; }
        public long UserId { get; set; }


        public string Token { get; set; }
        public List<string> Roles { get; set; }


        public long TokenExpires { get; set; }
    }
}
