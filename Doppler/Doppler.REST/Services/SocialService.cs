using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Social;
using Doppler.REST.Models.Repository;

namespace Doppler.REST.Services
{
    public class SocialService
    {
        private readonly IRepository repository;
        private readonly IdentityService identityService;
        public SocialService(IRepository repository, IdentityService identityService)
        {
            this.repository = repository;
            this.identityService = identityService;
        }

        public async Task<List<User>> SearchUsersAsync(string keyWord)
        {
            return await this.repository.SearchUsersByWordAsync(keyWord);
        }

        public async Task<List<UserContact>> GetUserContacts(int? skip = 0, int? take = null)
        {
            return await repository.GetUserContacts(identityService.AuthenticatedUser, skip, take);
        }

        public async Task<User> GetContactAsync(string login)
        {
            return await this.repository.GetContactAsync(login);
        }
    }
}
