using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Social;

namespace Doppler.API.Authentication
{
    public class SignedInUser
    {
        public User User { get; set; }
        public JwtToken AccessToken { get; set; }
        public JwtToken RefreshToken { get; set; }
    }
}
