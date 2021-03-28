using Doppler.REST.Models.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Storage;
using Doppler.API.Storage.FileStorage;

namespace Doppler.REST.Models.Repository
{
    public class ApplicationRepository : IRepository
    {
        private readonly ApplicationDatabaseContext databaseContext;

        private async Task<bool> CheckIsUserDuplicateAsync(DopplerUser dopplerUser)
        {
            return await this.databaseContext.DopplerUsers.AnyAsync(x =>
                x.PhoneNumber == dopplerUser.PhoneNumber || x.Email == dopplerUser.Email ||
                x.Login == dopplerUser.Login);
        }
        public ApplicationRepository(ApplicationDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<DopplerUser> GetDopplerUserWithPassword(string login)
        {
            var dopplerUser = await databaseContext.DopplerUsers.Include(x => x.Password).Include(x => x.RefreshToken).Include(x => x.Icon)
                    .FirstOrDefaultAsync(x => x.PhoneNumber == login);
            return dopplerUser;
        }
        
        public async Task<bool> AddUserAsync(DopplerUser dopplerUser)
        {
            if (!(await CheckIsUserDuplicateAsync(dopplerUser)))
            {
                await this.databaseContext.DopplerUsers.AddAsync(dopplerUser);
                await this.databaseContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<JwtToken> AssignNewRefreshTokenAsync(DopplerUser dopplerUser, JwtToken jwtToken)
        {
            if (dopplerUser.RefreshToken != null)
            {
                this.databaseContext.RefreshTokens.Remove(dopplerUser.RefreshToken);
            }

            dopplerUser.RefreshToken = jwtToken;
            this.databaseContext.DopplerUsers.Update(dopplerUser);
            await this.databaseContext.SaveChangesAsync();
            return jwtToken;
        }

        public async Task<List<User>> SearchUsersByWordAsync(string keyWord)
        {
            return await this.databaseContext.Users.Where(x =>
                    x.Name.Contains(keyWord) || x.Email.Contains(keyWord) || x.Login.Contains(keyWord) ||
                    x.PhoneNumber == keyWord)
                .Include(x => x.Icon).ToListAsync();
        }

        public async Task<List<UserContact>> GetUserContacts(User user, int? skip = 0, int? take = null)
        {
            var userContactsQuery = this.databaseContext.UsersContacts
                .Include(x => x.User)
                .Include(x => x.Contact).ThenInclude(x => x.Icon)
                .Where(x => x.User.Id == user.Id);
            userContactsQuery = userContactsQuery.SkipTake(skip, take);
            return await userContactsQuery.ToListAsync();
        }

        public async Task<Data> GetFileData(Guid Id)
        {
            return await this.databaseContext.Files.Include(x => x.BLOB).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<User> GetContactAsync(string login)
        {
            return await this.databaseContext.Users.Include(x => x.Icon).FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}
