using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Social;
using Doppler.API.Social.Chatting;
using Doppler.API.Social.Likes;
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

        public async Task<UserContact> GetUserContact(string login)
        {
            return await repository.GetUserContactAsync(this.identityService.AuthenticatedUser, login);
        }
        public async Task<User> GetContactAsync(string login)
        {
            return await this.repository.GetContactAsync(login);
        }

        public async Task AddToContacts(string login, string displayname = null)
        {
            await this.repository.AddToContacts(this.identityService.AuthenticatedUser, login, displayname);
        }

        public async Task<LikeResult> RateProfile(string login, bool like)
        {
            return await this.repository.RateProfile(this.identityService.AuthenticatedUser, login, like);
        }

        public async Task<bool> CheckUserForLike(string login)
        {
            return await this.repository.CheckUserForLike(this.identityService.AuthenticatedUser, login);
        }

        public async Task<Guid> GetDialogueInstanceId(string login)
        {
            return await this.repository.GetDialogueInstanceId(this.identityService.AuthenticatedUser, login);
        }

        public async Task<List<Conversation>> GetUserConversations(int? skip = 0, int? take = null)
        {
            return await this.repository.GetUserConversationsAsync(this.identityService.AuthenticatedUser, skip, take);
        }

        public async Task<List<ConversationMessage>> GetConversationMessages(Guid chatId, int? skip = 0,
            int? take = null)
        {
            return await this.repository.GetConversationMessages(this.identityService.AuthenticatedUser, chatId, skip,
                take);
        }

        public async Task<ConversationMessage> WriteMessageToChat(Guid chatId, ConversationMessage message)
        {
            return await this.repository.WriteMessageToChat(this.identityService.AuthenticatedUser, chatId, message);
        }
    }
}
