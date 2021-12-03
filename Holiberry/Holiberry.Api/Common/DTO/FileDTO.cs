using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Holiberry.Api.Common.DTO
{
    public class FileDTO
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public Stream Content { get; set; }
    }
}
