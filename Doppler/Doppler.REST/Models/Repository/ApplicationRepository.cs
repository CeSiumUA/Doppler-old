using Doppler.REST.Models.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Doppler.REST.Models.Repository
{
    public class ApplicationRepository : IRepository
    {
        private readonly ApplicationDatabaseContext databaseContext;

        public ApplicationRepository(ApplicationDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<DopplerUser> GetDopplerUserWithPassword(string login)
        {
            return await databaseContext.DopplerUsers.Include(x => x.Password)
                .FirstOrDefaultAsync(x => x.Email == login || x.PhoneNumber == login || x.Login == login);
        }
    }
}
