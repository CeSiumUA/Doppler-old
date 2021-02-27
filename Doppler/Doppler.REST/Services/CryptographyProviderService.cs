using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.REST.Models.Authentication;
using Doppler.REST.Models.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Doppler.REST.Services
{
    public class CryptographyProviderService : ICryptographyProvider
    {
        private readonly IConfiguration configuration;
        public CryptographyProviderService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool CompareHash(Password passwordHash, string plainText)
        {
            return HashProvider.CompareHash(plainText, passwordHash);
        }
        public JwtToken GenerateJwtToken(string login)
        {
            var tokenOptions = JwtTokenExtension.GetJwtOptions(configuration);
            var signingkey = JwtTokenExtension.GetSecurityKey(configuration);
            var currentTime = DateTime.Now;
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, login)
                }),
                Issuer = tokenOptions.Issuer,
                Audience = tokenOptions.Audience,
                Expires = currentTime.AddMinutes(tokenOptions.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingkey), SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            JwtToken jwtToken = new JwtToken()
            {
                ExpireDate = tokenDescriptor.Expires.HasValue
                    ? tokenDescriptor.Expires.Value
                    : currentTime.AddMinutes(tokenOptions.TokenLifeTime),
                IssueDate = currentTime,
                Token = tokenHandler.WriteToken(securityToken)
            };
            return jwtToken;
        }

        public Password HashPassword(string plainText)
        {
            return HashProvider.GenerateHash(plainText);
        }
    }
}
