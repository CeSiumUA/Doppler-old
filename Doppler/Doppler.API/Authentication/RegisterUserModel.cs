using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Doppler.API.Authentication
{
    public class RegisterUserModel
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
