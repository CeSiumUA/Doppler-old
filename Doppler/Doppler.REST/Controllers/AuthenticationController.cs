using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Storage;
using Doppler.REST.Services;

namespace Doppler.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService authenticationService;
        public AuthenticationController(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUserModel authenticateUserModel)
        {
            var authenticationResult = await this.authenticationService.Authenticate(authenticateUserModel);
            return new JsonResult(authenticationResult);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserModel registerUserModel)
        {
            var registeredUser = await this.authenticationService.RegisterUserTask(registerUserModel);
            return new JsonResult(registeredUser);
        }
    }
}
