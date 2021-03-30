using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.REST.Services;
using Microsoft.AspNetCore.SignalR;

namespace Doppler.REST.Models.Authentication
{
    public class IdentityHubFilter : IHubFilter
    {
        private readonly IdentityService identityService;

        public IdentityHubFilter(IdentityService identityService)
        {
            this.identityService = identityService;
        }
        public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invokationContext,
            Func<HubInvocationContext, ValueTask<object>> next)
        {
            await identityService.Authenticate(invokationContext.Context.User.Identity.Name);
            return await next(invokationContext);
        }
    }
}
