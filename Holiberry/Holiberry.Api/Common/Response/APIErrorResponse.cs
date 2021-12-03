using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Common.Response
{
    public class APIErrorResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public List<APIError> Errors { get; set; }
    }
}
