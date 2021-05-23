using System;
using System.Collections.Generic;
using System.Text;

namespace Doppler.API.Authentication
{
    public class AuthenticateUserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid DeviceId { get; set; }
    }
}
