using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Config
{
    public static class ConfigAuth
    {
        public static string IssuerSigningKey { get; set; }
        public static string ValidAudience { get; set; }
        public static string ValidIssuer { get; set; }

    }
}
