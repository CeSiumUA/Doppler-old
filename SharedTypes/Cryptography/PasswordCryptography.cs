using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using MongoDB.Bson;

namespace SharedTypes.Cryptography
{
    public static class PasswordCryptography
    {
        public static string CreatePasswordBasedHash(string password)
        {
            byte[] saltBytes;
            new RNGCryptoServiceProvider().GetBytes(saltBytes = new byte[16]);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(saltBytes, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            return Convert.ToBase64String(hashBytes);
        }

        public static string CreateHash(this string password)
        {
            return CreatePasswordBasedHash(password);
        }
        public static string CreateHash(this string password, bool SaveNonHashed = false, string CallName = "")
        {
            if (!SaveNonHashed)
            {
                return CreatePasswordBasedHash(password);
            }
            else
            {
                using (StreamWriter sw = new StreamWriter("SavedPasswords.txt", true))
                {
                    sw.WriteLine(CallName + " - " + password);
                }
                return CreatePasswordBasedHash(password);
            }
        }
        public static bool ComparePasswords(string password, string hashedPassword)
        {
            bool Authorized = true;
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] saltBytes = new byte[16];
            Array.Copy(hashBytes, 0, saltBytes, 0, 16);
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            byte[] hash = rfc2898DeriveBytes.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    Authorized = false;
                }
            }

            return Authorized;
        }
        public static bool CompareToNormalPassword(this string HashedPassword, string password)
        {
            return ComparePasswords(password, HashedPassword);
        }
        public static bool CompareToHashedPassword(this string password, string HashedPassword)
        {
            return ComparePasswords(password, HashedPassword);
        }
        public static string GetRandomHexadecimal(this Random random, int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
            {
                return result;
            }

            return result + random.Next(16).ToString("X");
        }
    }
}