using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Storage;
using Doppler.REST.Models.Authentication;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Models.Repository;
using Doppler.REST.Models.Social;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doppler.REST.Services
{
    public class AuthenticationService
    {
        private readonly IRepository repository;
        private readonly ICryptographyProvider cryptographyProvider;
        private readonly IdentityService identityService;

        public AuthenticationService(IRepository repository, ICryptographyProvider cryptographyProvider, IdentityService identityService)
        {
            this.repository = repository;
            this.cryptographyProvider = cryptographyProvider;
            this.identityService = identityService;
        }

        private async Task<DopplerUser> GetUserWithPasswordAsync(string login, string password)
        {
            var userWithPassword = await repository.GetDopplerUserWithPassword(login);
            if (userWithPassword != null && !cryptographyProvider.CompareHash(userWithPassword.Password, password))
            {
                return null;
            }

            return userWithPassword;
        }

        public async Task<SignedInUser> Authenticate(string userName, string password)
        {
            var retreivedUser = await GetUserWithPasswordAsync(userName, password);
            if (retreivedUser == null)
            {
                return null;
            }

            return await GenerateUserAccess(retreivedUser);
        }

        private async Task<SignedInUser> GenerateUserAccess(DopplerUser user)
        {
            var accessJwtToken = await AssignTokenAsync(user.PhoneNumber);
            var refreshToken = await AssignRefreshTokenAsync(user);
            return user.GetSignedInUser(accessJwtToken, refreshToken);
        }

        public async Task<SignedInUser> Authenticate(AuthenticateUserModel authenticateUserModel)
        {
            return await Authenticate(authenticateUserModel.UserName, authenticateUserModel.Password);
        }

        private async Task<JwtToken> AssignTokenAsync(string login)
        {
            return this.cryptographyProvider.GenerateJwtToken(login);
        }

        private async Task<JwtToken> AssignRefreshTokenAsync(DopplerUser dopplerUser)
        {
            var refreshToken = this.cryptographyProvider.GenerateRefreshToken();
            return await this.repository.AssignNewRefreshTokenAsync(dopplerUser, refreshToken);
        }

        public async Task<SignedInUser> RegisterUserTask(RegisterUserModel registerUserModel)
        {
            DopplerUser registeredUser = DopplerUser.InitializeNewUser(registerUserModel, this.cryptographyProvider);
            var userAdded = await this.repository.AddUserAsync(dopplerUser: registeredUser);
            if (!userAdded)
            {
                return await Authenticate(registerUserModel.PhoneNumber, registerUserModel.Password);
            }

            var token = await AssignTokenAsync(registeredUser.PhoneNumber);
            var refreshToken = await AssignRefreshTokenAsync(registeredUser);
            return registeredUser.GetSignedInUser(token, refreshToken);
        }

        public async Task<SignedInUser> ChangeRefreshToken(string token)
        {
            token = token.Replace("Bearer ", string.Empty);
            if (identityService.AuthenticatedUser == null)
            {
                return null;
            }
            if (identityService.AuthenticatedUser.RefreshToken.Token == token && identityService.AuthenticatedUser.RefreshToken.ExpireDate > DateTime.Now)
            {
                return await GenerateUserAccess(identityService.AuthenticatedUser);
            }
            else
            {
                return null;
            }
        }
    }
}
