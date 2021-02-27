using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Storage;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Models.Repository;
using Doppler.REST.Models.Social;
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

            var accessJwtToken = await AssignTokenAsync(retreivedUser.PhoneNumber);
            var refreshToken = await AssignRefreshTokenAsync(retreivedUser as DopplerUser);
            return retreivedUser.GetSignedInUser(accessJwtToken, refreshToken);
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
            var refreshToken = this.cryptographyProvider.GenerateRefreshToken(dopplerUser.PhoneNumber);
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
    }
}
