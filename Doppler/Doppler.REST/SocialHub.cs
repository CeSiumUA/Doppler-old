using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Social;
using Doppler.REST.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Doppler.REST
{
    [Authorize]
    public class SocialHub : Hub
    {
        private readonly SocialService socialService;
        private readonly IdentityService identityService;
        public SocialHub(SocialService socialService, IdentityService identityService)
        {
            this.socialService = socialService;
            this.identityService = identityService;
        }

        public async Task<User> GetUser(string login)
        {
            return await socialService.GetContactAsync(login);
        }

        public async Task<UserContact> GetContact(string login)
        {
            return await socialService.GetUserContact(login);
        }
        public async Task<List<User>> SearchUsers(string pattern)
        {
            return await socialService.SearchUsersAsync(pattern);
        }

        public async Task<List<UserContact>> GetUserContacts(int? skip = 0, int? take = null)
        {
            return await socialService.GetUserContacts();
        }

        public async Task AddToContacts(string login, string displayName = null)
        {
            await socialService.AddToContacts(login, displayName);
        }
    }
}
