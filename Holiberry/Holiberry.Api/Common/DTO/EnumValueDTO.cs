using System;
using System.Collections.Generic;
using System.Text;

namespace Holiberry.Api.Common.DTO
{
    public class EnumValueDTO
    {
        public EnumValueDTO()
        { 
        }

        public EnumValueDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
