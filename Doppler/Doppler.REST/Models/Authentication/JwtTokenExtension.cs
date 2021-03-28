using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppler.REST.Models.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
            jwtBearerOptions.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    // If the request is for our hub...
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/socialHub")))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
            jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = jwt.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(securityKey)
            };
            return jwtBearerOptions;
        }

        public static JwtTokenOptions GetJwtOptions(IConfiguration configuration)
        {
            var jwtTokenConfig = new JwtTokenOptions();
            configuration.GetSection(JwtTokenOptions.JwtToken).Bind(jwtTokenConfig);
            return jwtTokenConfig;
        }

        public static byte[] GetSecurityKey(IConfiguration configuration)
        {
            return Encoding.ASCII.GetBytes(configuration["Doppler:TokenIssuerSigningKey"]);
        }
    }
}
