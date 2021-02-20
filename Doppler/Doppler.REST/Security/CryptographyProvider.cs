using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Security;

namespace Doppler.REST.Security
{
    public class CryptographyProvider : ICryptographyProvider
    {
        public bool CompareHash(string hashText, string plainText)
        {
            throw new NotImplementedException();
        }

        public string HashString(string plainText)
        {
            throw new NotImplementedException();
        }
    }
}
