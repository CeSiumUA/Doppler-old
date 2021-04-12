using Doppler.REST.Models.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Social.Chatting;
using Doppler.API.Storage;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Social.Likes;

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
            var dopplerUser = await databaseContext.DopplerUsers.Include(x => x.Password).Include(x => x.RefreshToken).Include(x => x.Icons)
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
            keyWord = keyWord.ToLower();
            return await this.databaseContext.Users.Where(x =>
                    x.Name.ToLower().Contains(keyWord) || x.Email.ToLower() == (keyWord) || x.Login.ToLower().Contains(keyWord) ||
                    x.PhoneNumber.ToLower() == keyWord)
                .Include(x => x.Icons).ToListAsync();
        }

        public async Task<List<UserContact>> GetUserContacts(User user, int? skip = 0, int? take = null)
        {
            var userContactsQuery = this.databaseContext.UsersContacts
                .Include(x => x.User)
                .Include(x => x.Contact).ThenInclude(x => x.Icons)
                .Where(x => x.User.Id == user.Id);
            userContactsQuery = userContactsQuery.SkipTake(skip, take);
            return await userContactsQuery.ToListAsync();
        }

        public async Task<UserContact> GetUserContactAsync(User user, string login)
        {
            return await this.databaseContext.UsersContacts.Include(x => x.User)
                .Include(x => x.Contact).ThenInclude(x => x.Icons)
                .Include(x => x.Contact).ThenInclude(x => x.UserLikes).ThenInclude(x => x.LikedUser)
                .FirstOrDefaultAsync(x => x.Contact.Login == login && x.User.Id == user.Id);
        }
        public async Task<Data> GetFileData(Guid Id)
        {
            return await this.databaseContext.Files.Include(x => x.BLOB).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<User> GetContactAsync(string login)
        {
            return await this.databaseContext.Users.Include(x => x.Icons)
                .Include(x => x.UserLikes).ThenInclude(x => x.LikedUser)
                .FirstOrDefaultAsync(x => x.Login == login);
        }

        public async Task AddToContacts(User user, string login, string displayName = null)
        {
            UserContact userContact = new UserContact();
            userContact.DisplayName = displayName;
            userContact.User = user;
            userContact.Contact = await this.databaseContext.Users.FirstOrDefaultAsync(x => x.Login == login);
            await this.databaseContext.UsersContacts.AddAsync(userContact);
            await this.databaseContext.SaveChangesAsync();
        }

        public async Task<LikeResult> RateProfile(User user, string login, bool like)
        {
            var likeQuery = await this.databaseContext.UsersLikes
                .Include(x => x.Liker)
                .Include(x => x.LikedUser)
                .FirstOrDefaultAsync(x => x.LikedUser.Login == login && x.Liker.Id == user.Id);
            if (likeQuery == null)
            {
                likeQuery = new UserLike()
                {
                    LikedUser = await this.databaseContext.Users.FirstOrDefaultAsync(x => x.Login == login),
                    Liker = user,
                };
                await this.databaseContext.AddAsync(likeQuery);
            }

            likeQuery.IsLiked = like;
            await this.databaseContext.SaveChangesAsync();
            LikeResult likeResult =  new LikeResult()
            {
                IsLiked = like,
                Likes = await this.databaseContext.UsersLikes
                    .Include(x => x.LikedUser)
                    .LongCountAsync(x => x.IsLiked && x.LikedUser.Login == login)
            };
            return likeResult;
        }

        public async Task<bool> CheckUserForLike(User user, string login)
        {
            var userLike = await this.databaseContext.UsersLikes
                .Include(x => x.LikedUser)
                .Include(x => x.Liker)
                .FirstOrDefaultAsync(x => x.LikedUser.Login == login && x.Liker.Id == user.Id);
            if (userLike == null)
            {
                return false;
            }
            return userLike.IsLiked;
        }

        public async Task<Guid> GetChatInstance(User user, string login)
        {
            var dialogue = await this.databaseContext
                .Conversations.Include(x => x.Members).ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.IsDialogue
                                          && x.Members.Exists(x => x.User.Id == user.Id)
                                          && x.Members.Exists(x => x.User.Login == login));
            if (dialogue == null)
            {
                dialogue = new Conversation()
                {

                };
            }

            return dialogue.Id;
        }
    }
}
