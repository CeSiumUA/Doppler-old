using Doppler.REST.Models.Social;
using System;
using System.Collections.Generic;
using System.IO;
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
using Doppler.API.Storage.UserStorage;
using Microsoft.AspNetCore.Http;

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
                //FIXME
                await this.databaseContext.SaveChangesAsync();
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

        public async Task<Guid> GetDialogueInstanceId(User user, string login)
        {
            var dialogue = await RetreiveDialogue(user, login);
            return dialogue.Id;
        }

        private async Task<Dialogue> RetreiveDialogue(User user, string login, bool includeImageToQuery = true)
        {
            var opponent = await this.databaseContext.UsersContacts.Include(x => x.Contact).Include(x => x.User).FirstOrDefaultAsync(x => x.User.Id == user.Id && x.Contact.Login == login);
            string opponentDisplayName = opponent?.DisplayName;
            var opponentUser = opponent?.Contact;
            if (opponent == null)
            {
                opponentUser = await this.databaseContext.Users.FirstOrDefaultAsync(x => x.Login == login);
                opponentDisplayName = opponentUser?.Name;
            }

            Dialogue dialogue = null;
            if(includeImageToQuery)
            { 
                dialogue = await this.databaseContext.Dialogues
                .Include(x => x.FirstUser).ThenInclude(x => x.User).ThenInclude(x => x.Icons)
                .Include(x => x.SecondUser).ThenInclude(x => x.User).ThenInclude(x => x.Icons)
                .FirstOrDefaultAsync(x => (x.FirstUser.User.Id == user.Id && x.SecondUser.User.Id == opponentUser.Id) || (x.SecondUser.User.Id == user.Id && x.FirstUser.User.Id == opponentUser.Id));
            }
            else
            {
                dialogue = await this.databaseContext.Dialogues
                    .Include(x => x.FirstUser).ThenInclude(x => x.User)
                    .Include(x => x.SecondUser).ThenInclude(x => x.User)
                    .FirstOrDefaultAsync(x => (x.FirstUser.User.Id == user.Id && x.SecondUser.User.Id == opponentUser.Id) || (x.SecondUser.User.Id == user.Id && x.FirstUser.User.Id == opponentUser.Id));
            }
            if (dialogue == null)
            {
                dialogue = new Dialogue();
                await this.databaseContext.Conversations.AddAsync(dialogue);
                await this.databaseContext.SaveChangesAsync();
                dialogue.FirstUser = new ConversationMember()
                {
                    Conversation = dialogue,
                    Role = ConversationMemberRole.Member,
                    User = user
                };
                dialogue.SecondUser = new ConversationMember()
                {
                    Conversation = dialogue,
                    DisplayName = opponentDisplayName,
                    Role = ConversationMemberRole.Member,
                    User = opponentUser
                };
                await this.databaseContext.SaveChangesAsync();
            }

            return dialogue;
        }
        public async Task<Guid> SetProfileImage(User user, IFormFile formFile)
        {
            var images = (await this.databaseContext.Users.Include(x => x.Icons)
                .FirstOrDefaultAsync(x => x.Id == user.Id)).Icons;
            ProfileImage profileImage;
            await using (MemoryStream ms = new MemoryStream())
            {
                await formFile.CopyToAsync(ms);
                profileImage = new ProfileImage()
                {
                    IsActive = true,
                    BLOB = new BLOB()
                    {
                        Data = ms.ToArray()
                    },
                    ContentType = formFile.ContentType,
                    FileName = formFile.FileName,
                    FileSize = formFile.Length,
                    User = user
                };
            }
            images.ForEach(x => x.IsActive = false);
            images.Add(profileImage);
            //await this.databaseContext.ProfileImages.AddAsync(profileImage);
            await this.databaseContext.SaveChangesAsync();
            return profileImage.Id;
        }

        private async Task<Data> UploadData(MemoryStream fileStream, string? fileName = null, string? contentType = null,
            long? fileLength = 0)
        {
            Data fileData = new Data()
            {
                ContentType = contentType,
                FileName = fileName,
                FileSize = fileLength,
                BLOB = new BLOB()
                {
                    Data = fileStream.ToArray()
                }
            };
            return fileData;
        }

        public async Task<List<Conversation>> GetUserConversationsAsync(User user, int? skip = 0, int? take = null)
        {
            var conversationMembers = await this.databaseContext.ConversationMembers.Include(x => x.User).Include(x => x.Conversation)
                .Where(x => x.User.Id == user.Id).Select(x => x.Conversation).ToListAsync();
            List<Conversation> conversations = new List<Conversation>();

            foreach (var conversation in conversationMembers)
            {
                if (conversation is Dialogue)
                {
                    var dialogue = await this.databaseContext.Dialogues
                        .Include(x => x.FirstUser).ThenInclude(x => x.User).ThenInclude(x => x.Icons)
                        .Include(x => x.SecondUser).ThenInclude(x => x.User).ThenInclude(x => x.Icons)
                        .FirstOrDefaultAsync(x => x.Id == conversation.Id);
                    dialogue.SetUserContext(user);
                    conversations.Add(dialogue);
                    continue;
                }
                conversations.Add(conversation);
            }

            return conversations;
        }

        public async Task<List<ConversationMessage>> GetConversationMessages(User user, Guid conversationId, int? skip = 0, int? take = null)
        {
            var conversationMember =
                await this.databaseContext.ConversationMembers.FirstOrDefaultAsync(x =>
                    x.ConversationId == conversationId && x.UserId == user.Id);
            var lastMessages = await this.databaseContext.ConversationMessages
                .Include(x => x.Sender)
                .Where(x => x.Sender.ConversationId == conversationId)
                .Include(x => x.Content).ThenInclude(x => x.MediaContents).ThenInclude(x => x.Data)
                .OrderByDescending(x => x.SentOn).SkipTake(skip, take).Reverse().ToListAsync();
            return lastMessages;
        }

        public async Task<ConversationMessage> WriteMessageToChat(User user, Guid chatId, ConversationMessage message)
        {
            var conversationMember =
                await this.databaseContext.ConversationMembers.FirstOrDefaultAsync(x =>
                    x.UserId == user.Id && x.ConversationId == chatId);
            if (conversationMember == null) return null;
            message.Sender = conversationMember;
            message.SentOn = DateTime.UtcNow;
            await this.databaseContext.ConversationMessages.AddAsync(message);
            await this.databaseContext.SaveChangesAsync();
            return message;
        }

        public async Task<Conversation> GetUserConversationAsync(User user, Guid id)
        {
            var conversation = await this.databaseContext.ConversationMembers.Include(x => x.User).Include(x => x.Conversation)
                .Where(x => x.User.Id == user.Id).Select(x => x.Conversation).FirstOrDefaultAsync(x => x.Id == id);

            if (conversation != null)
            {
                if (conversation is Dialogue)
                {
                    var dialogue = await this.databaseContext.Dialogues
                        .Include(x => x.FirstUser).ThenInclude(x => x.User).ThenInclude(x => x.Icons)
                        .Include(x => x.SecondUser).ThenInclude(x => x.User).ThenInclude(x => x.Icons)
                        .FirstOrDefaultAsync(x => x.Id == conversation.Id);
                    dialogue.SetUserContext(user);
                    return dialogue;
                }
            }

            return conversation;
        }

        public async Task<IEnumerable<string>> GetConversationMembersPhones(Guid id)
        {
            return this.databaseContext.ConversationMembers.Where(x => x.ConversationId == id).Include(x => x.User).Select(x => x.User.PhoneNumber);
        }
    }
}
