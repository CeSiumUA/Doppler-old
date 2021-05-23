using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doppler.REST.Models.Authentication
{
    public class JwtTokenOptions
    {
        public const string JwtToken = "JwtToken";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenLifeTime { get; set; }
        public int RefreshTokenLifeTimeInDays { get; set; }
    }
}
