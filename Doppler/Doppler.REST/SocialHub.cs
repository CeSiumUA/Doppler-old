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
    }
}
