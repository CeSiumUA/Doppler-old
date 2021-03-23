using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.REST.Models.Repository;

namespace Doppler.REST.Services
{
    public class SocialService
    {
        private readonly IRepository repository;
        private readonly IdentityService identityService;
        public SocialService(IRepository repository, IdentityService identityService)
        {
            this.repository = repository;
            this.identityService = identityService;
        }
    }
}
