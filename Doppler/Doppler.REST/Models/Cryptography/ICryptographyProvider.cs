using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Authentication;
using Doppler.API.Social;

namespace Doppler.REST.Models.Cryptography
{
    public interface ICryptographyProvider
    {
        public Password HashPassword(string plainText);
        public bool CompareHash(Password passwordHash, string plainText);
        public JwtToken GenerateJwtToken(string login);
    }
}
