using Holiberry.Api.Models.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Models.Files.Entities
{
    public class DbFileM : EntityBaseM
    {

        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }



    }
}
