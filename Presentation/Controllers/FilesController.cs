﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // folder
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            // path (eger ki Media altinda baska klasorleme yapilacaksa burada yapilacak)
            var path = Path.Combine(folder, file.FileName);

            // stream
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // response body
            return Ok(new
            {
                file = file.FileName,
                path = path,
                size = file.Length,
            });
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download(string fileName)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // filePath
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Media/Blogs", fileName);

            // ContentType : (MIME)
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            // Read
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, contentType, Path.GetFileName(filePath));
        }

        [HttpGet("getAll")]
        public IActionResult GetAllFiles()
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");

            if (!Directory.Exists(folder))
                return NotFound("Media folder not found.");

            var files = Directory.GetFiles(folder)
                                 .Select(filePath => new FileInfo(filePath))
                                 .Select(fileInfo => new
                                 {
                                     Name = fileInfo.Name,
                                     Path = fileInfo.FullName,
                                     Size = fileInfo.Length
                                 })
                                 .ToList();

            return Ok(files);
        }
    }
}

