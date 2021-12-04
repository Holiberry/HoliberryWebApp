using Holiberry.Api.Attributes;
using Holiberry.Api.Config;
using Holiberry.Api.Managers.File;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holiberry.Api.Areas.Api.Controllers
{
    [ApiAuthorize]
    [TypeFilter(typeof(ApiExceptionFilterAttribute))]
    [Area(AreasConfig.Public)]
    [Route("v1/public/files")]
    public class P_FilesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IDbFileService _dbFileService;

        public P_FilesController(
            ApplicationDbContext db, 
            IDbFileService dbFileService
            )
        {
            _db = db;
            _dbFileService = dbFileService;
        }



        //[HttpGet("user-photo")]
        //public async Task<IActionResult> GetUserPhoto(long photoId)
        //{
        //    var userPhoto = await _db.UserPhotos.AsNoTracking()
        //        .Where(a => a.Id == photoId)
        //        .FirstOrDefaultAsync();

        //    var file = await _dbFileService.GetDbFile(userPhoto.FileId);

        //    return File(file.Content, file.ContentType);
        //}


    }
}
