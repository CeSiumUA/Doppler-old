using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.REST.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doppler.REST.Models.Authentication
{
    public class AuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly IdentityService identityService;
        public AuthenticationFilter(IdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var identityName = context.HttpContext.User.Identity?.Name;
            if (identityName != null)
            {
                await this.identityService.Authenticate(identityName);
            }
        }
    }
}
