using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Common.Response
{
    public class APIError
    {
        public string Code { get; set; }
        public string Message { get; set; }


        public APIError(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
