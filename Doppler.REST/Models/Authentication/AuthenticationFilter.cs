using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace Doppler.REST.Models.Authentication
{
    public class AuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly IdentityService identityService;
        private readonly ICryptographyProvider cryptographyProvider;
        public AuthenticationFilter(IdentityService identityService, ICryptographyProvider cryptographyProvider)
        {
            this.identityService = identityService;
            this.cryptographyProvider = cryptographyProvider;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identityName = context.HttpContext.User.Identity?.Name;
            var actionName = context.RouteData.Values.Values.FirstOrDefault().ToString();
            if (identityName != null)
            {
                await this.identityService.Authenticate(identityName);
            }
            else if(actionName == "RecoverAccess")
            {
                
                var authHeaders = context.HttpContext.Request.Headers["Authorization"];
                await CheckForOudatedTokenAsync(authHeaders.FirstOrDefault());
            }
        }

        private async Task CheckForOudatedTokenAsync(string token)
        {
            var claims = cryptographyProvider.GetIdentityFromOutdatedToken(token);
            await this.identityService.Authenticate(claims);
        }
    }
}
