using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Security;
using Doppler.API.Social;
using Doppler.API.Storage;
using Doppler.REST.Models.Repository;

namespace Doppler.REST.Services
{
    public class AuthenticationService
    {
        private readonly IRepository repository;
        private readonly ICryptographyProvider cryptographyProvider;
        public AuthenticationService(IRepository repository, ICryptographyProvider cryptographyProvider)
        {
            this.repository = repository;
            this.cryptographyProvider = cryptographyProvider;
        }

        internal User GetUserWithPassword(string login, string password)
        {
            repository.GetDopplerUserWithPassword(login)
        }
    }
}
