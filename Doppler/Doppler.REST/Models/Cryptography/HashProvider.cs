using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Doppler.API.Social;

namespace Doppler.REST.Models.Cryptography
{
    public static class HashProvider
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100000;
        public static Password GenerateHash(string input)
        {
            Password password = new Password();
            using (var algorithm = new Rfc2898DeriveBytes(
                input, SaltSize, Iterations, HashAlgorithmName.SHA256))
            {
                password.PasswordHash = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                password.PasswordSalt = Convert.ToBase64String(algorithm.Salt);
                password.Iterations = Iterations;
            }
            return password;
        }

        public static bool CompareHash(string rawValue, Password passwordHash)
        {
            bool verified = false;
            using (var algoritm = new Rfc2898DeriveBytes(
                rawValue, 
                passwordHash.GetSaltBytes(),
                passwordHash.Iterations,
                HashAlgorithmName.SHA256))
            {
                var keyToCheck = algoritm.GetBytes(KeySize);

                verified = keyToCheck.SequenceEqual(passwordHash.GetHashBytes());
            }

            return verified;
        }
    }
}
