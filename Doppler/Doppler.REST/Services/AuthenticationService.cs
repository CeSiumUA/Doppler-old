using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Security;
using Doppler.API.Social;
using Doppler.API.Storage;
using Doppler.REST.Models.Repository;
using Microsoft.AspNetCore.Mvc.Filters;

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

        private async Task<User> GetUserWithPasswordAsync(string login, string password)
        {
            var userWithPassword = await repository.GetDopplerUserWithPassword(login);
            if (userWithPassword != null && !cryptographyProvider.CompareHash(userWithPassword.Password.PasswordHash, password))
            {
                return null;
            }
            return userWithPassword;
        }

        public async Task<SignedInUser> Authenticate(AuthenticateUserModel authenticateUserModel)
        {
            var retreivedUser = await GetUserWithPasswordAsync(authenticateUserModel.UserName, authenticateUserModel.Password);
            if (retreivedUser == null)
            {
                return null;
            }

            var jwtToken = cryptographyProvider.GenerateJwtToken(retreivedUser.Login);
            return new SignedInUser()
            {
                Token = jwtToken,
                User = retreivedUser
            };
        }
    }
}
