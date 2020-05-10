﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes.Tokens
{
    public class AuthenticationToken
    {
        public const string Issuer = "kit.Messaging.server";
        public const string Audience = "kit.messenger.users";
        public static string EncryptionKey = "12345";
        public const int TTL = 120;
        public static SymmetricSecurityKey GetEncryptionKey()
        {
            using(System.IO.StreamReader sr = new System.IO.StreamReader("key.rcc"))
            {
                EncryptionKey = sr.ReadLine();
            }
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EncryptionKey));
        }
    }
}
