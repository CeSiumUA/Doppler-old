using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Storage.FileStorage;
using Doppler.REST.Models.Social;

namespace Doppler.REST.Models.Repository
{
    public interface IRepository
    {
        public Task<DopplerUser> GetDopplerUserWithPassword(string login);
        public Task<bool> AddUserAsync(DopplerUser dopplerUser);
        public Task<JwtToken> AssignNewRefreshTokenAsync(DopplerUser dopplerUser, JwtToken jwtToken);
        public Task<List<User>> SearchUsersByWordAsync(string keyWord);
        public Task<List<UserContact>> GetUserContacts(User user, int? skip = 0, int? take = null);
        public Task<Data> GetFileData(Guid Id);
        public Task<User> GetContactAsync(string login);
    }
}
