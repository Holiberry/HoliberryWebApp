using Holiberry.Api.Common.DTO;
using Holiberry.Api.Managers.File;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Models.Files.Entities;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holiberry.Api.Managers.File
{
    public class DbFileService : IDbFileService
    {
        private readonly ApplicationDbContext _db;

        public DbFileService(ApplicationDbContext db)
        {
            _db = db;
        }





       


    }
}
