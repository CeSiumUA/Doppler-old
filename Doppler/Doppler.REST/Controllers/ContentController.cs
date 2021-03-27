using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.REST.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;

namespace Doppler.REST.Controllers
{
    [Route("cdn/files")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IRepository repository;
        public ContentController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("{fileId}")]
        //[Authorize]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            var file = await repository.GetFileData(fileId);
            if (file == null)
            {
                return BadRequest();
            }

            return new FileContentResult(file.BLOB.Data, MediaTypeHeaderValue.Parse("image/png"));
        }
    }
}
