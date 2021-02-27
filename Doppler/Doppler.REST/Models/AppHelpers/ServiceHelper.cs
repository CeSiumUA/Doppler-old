using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Storage;
using Doppler.REST.Models.Authentication;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Models.Repository;
using Doppler.REST.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Doppler.REST.Models.AppHelpers
{
    public static class ServiceHelper
    {
        public static void AddCustomeServices(this IServiceCollection service)
        {
            service.AddScoped<AuthenticationService>();
            service.AddDbContext<ApplicationDatabaseContext>();
            service.AddScoped<IRepository, ApplicationRepository>();
            service.AddScoped<ICryptographyProvider, CryptographyProviderService>();
            service.AddScoped<IdentityService>();
        }
    }
}
