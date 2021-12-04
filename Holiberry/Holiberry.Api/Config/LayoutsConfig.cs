using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Config
{
    public class LayoutsConfig
    {
        public const string Admin = "~/Areas/Admin/Views/Shared/_Layout.cshtml";
        public const string Web = "~/Areas/Web/Views/Shared/_Layout.cshtml";

        public const string Clear = "~/Views/Shared/_LayoutClear.cshtml";
        public const string Redirect = "~/Views/Shared/_LayoutRedirect.cshtml";
    }
}
