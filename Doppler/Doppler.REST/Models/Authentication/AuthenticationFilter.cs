using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doppler.REST.Models.Authentication
{
    public class AuthenticationFilter : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var x = context.HttpContext.User;
            return Task.CompletedTask;
        }
    }
}
