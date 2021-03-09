using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private readonly JwtTokenOptions tokenOptions;
        private readonly IConfiguration configuration;
        public CryptographyProviderService(IConfiguration configuration)
        {
            this.configuration = configuration;
            tokenOptions = JwtTokenExtension.GetJwtOptions(configuration);
        }
        public bool CompareHash(Password passwordHash, string plainText)
        {
            return HashProvider.CompareHash(plainText, passwordHash);
        }

        public JwtToken GenerateJwtToken(string login, bool generateRefreshToken = false)
        {
            var signingkey = JwtTokenExtension.GetSecurityKey(configuration);
            var currentTime = DateTime.Now;
            int tokenLifeTime = tokenOptions.TokenLifeTime;
            string securityAlgorithm = SecurityAlgorithms.HmacSha256Signature;
            if (generateRefreshToken)
            {
                tokenLifeTime = tokenOptions.RefreshTokenLifeTimeInDays * 24 * 60;
                securityAlgorithm = SecurityAlgorithms.HmacSha512Signature;
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, login)
                }),
                Issuer = tokenOptions.Issuer,
                Audience = tokenOptions.Audience,
                Expires = currentTime.AddMinutes(tokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingkey), securityAlgorithm)
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            JwtToken jwtToken = new JwtToken()
            {
                ExpireDate = tokenDescriptor.Expires.HasValue
                    ? tokenDescriptor.Expires.Value
                    : currentTime.AddMinutes(tokenLifeTime),
                IssueDate = currentTime,
                Token = tokenHandler.WriteToken(securityToken)
            };
            return jwtToken;
        }

        private JwtToken GenerateRefreshTokenForUser()
        {
            JwtToken refreshToken = new JwtToken();
            var randomNumber = new byte[256];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            refreshToken.Token = Convert.ToBase64String(randomNumber);
            refreshToken.ExpireDate = DateTime.Now.AddDays(this.tokenOptions.RefreshTokenLifeTimeInDays);
            refreshToken.IssueDate = DateTime.Now;
            return refreshToken;
        }
        public JwtToken GenerateRefreshToken()
        {
            return GenerateRefreshTokenForUser();
        }
        public Password HashPassword(string plainText)
        {
            return HashProvider.GenerateHash(plainText);
        }
    }
}
