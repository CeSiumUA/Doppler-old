using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Storage.FileStorage;
using Doppler.REST.Models.Repository;
using Microsoft.AspNetCore.Http;

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

        public async Task<Guid> UploadFile(IFormFileCollection files, FileUploadType uploadType, FileAccessLevel fileAccessLevel)
        {
            Guid result = Guid.Empty;
            switch (uploadType)
            {
                case FileUploadType.ProfileImage:
                    result = await this.repository.SetProfileImage(identityService.AuthenticatedUser,
                        files.FirstOrDefault());
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
