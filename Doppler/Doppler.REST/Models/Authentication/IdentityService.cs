using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.REST.Models.Repository;
using Doppler.REST.Models.Social;

namespace Doppler.REST.Models.Authentication
{
    public class IdentityService
    {
        private readonly IRepository repository;
        public DopplerUser SignedInUser;
        public IdentityService(IRepository repository)
        {
            this.repository = repository;
        }
        public async Task Authenticate(string claim)
        {
            this.SignedInUser = await repository.GetDopplerUserWithPassword(claim);
        }
    }
}
