using System;
using System.Collections.Generic;
using System.Text;

namespace Doppler.API.Authentication
{
    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
