using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes.Tokens
{
    public class AuthenticationToken
    {
        public const string Issuer = "kit.Messaging.server";
        public const string Audience = "kit.messenger.users";
        public const int TTL = 2;
        public static SymmetricSecurityKey GetEncryptionKey(string key)
        {
           
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
