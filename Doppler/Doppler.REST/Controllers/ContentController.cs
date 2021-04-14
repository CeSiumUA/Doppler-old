using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.REST.Models.Repository;
using Doppler.REST.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;

namespace Doppler.REST.Controllers
{
    [Route("cdn/files")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly ContentService contentService;
        public ContentController(ContentService contentService)
        {
            this.contentService = contentService;
        }

        [HttpGet("{fileId}")]
        [Authorize]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            var file = await contentService.GetFileData(fileId);
            if (file == null)
            {
                return BadRequest();
            }

            return new FileContentResult(file.BLOB.Data, MediaTypeHeaderValue.Parse("image/png"));
        }
        [HttpPost("uploadfile")]
        [Authorize]
        public async Task<IActionResult> UploadFile()
        {
            var filesCollection = this.HttpContext.Request.Form.Files;
            return Ok();
        }
    }
}
