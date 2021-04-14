using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Storage.FileStorage;
using Doppler.REST.Models.Repository;

namespace Doppler.REST.Services
{
    public class ContentService
    {
        private readonly IdentityService identityService;
        private readonly IRepository repository;
        public ContentService(IdentityService identityService, IRepository repository)
        {
            this.identityService = identityService;
            this.repository = repository;
        }

        public async Task<Data> GetFileData(Guid Id)
        {
            var file = await repository.GetFileData(Id);
            return file;
        }

        public async Task UploadFile()
        {

        }
    }
}
