using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppler.REST.Models.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Doppler.REST.Models.Authentication
{
    public static class JwtTokenExtension
    {
        public static JwtBearerOptions LoadTokenOptions(this JwtBearerOptions jwtBearerOptions,
            IConfiguration configuration)
        {
            JwtTokenOptions jwt = GetJwtOptions(configuration);
            byte[] securityKey = GetSecurityKey(configuration);
            jwtBearerOptions.RequireHttpsMetadata = true;
            jwtBearerOptions.SaveToken = true;
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = jwt.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(securityKey)
            };
            return jwtBearerOptions;
        }

        private static JwtTokenOptions GetJwtOptions(IConfiguration configuration)
        {
            var jwtTokenConfig = new JwtTokenOptions();
            configuration.GetSection(JwtTokenOptions.JwtToken).Bind(jwtTokenConfig);
            return jwtTokenConfig;
        }

        private static byte[] GetSecurityKey(IConfiguration configuration)
        {
            return Encoding.ASCII.GetBytes(configuration["Doppler:TokenIssuerSigningKey"]);
        }
    }
}
