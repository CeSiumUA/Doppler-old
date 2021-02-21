using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Authentication;

namespace Doppler.API.Security
{
    public interface ICryptographyProvider
    {
        public string HashString(string plainText);
        public bool CompareHash(string hashText, string plainText);
        public JwtToken GenerateJwtToken(string login);
    }
}
