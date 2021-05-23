using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Social.Chatting;
using Doppler.API.Social.Likes;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Storage.UserStorage;

namespace Doppler.API.Storage
{
    public interface IDBContextEntities
    {
        #region DBSet
        public DbSet<User> Users { get; set; }
        public DbSet<JwtToken> RefreshTokens { get; set; }
        public DbSet<UserContact> UsersContacts { get; set; }
        #endregion

        #region FileStorage
        public DbSet<BLOB> Blobs { get; set; }
        public DbSet<Data> Files { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        #endregion
        #region SocialDbSet
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Dialogue> Dialogues { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ConversationMember> ConversationMembers { get; set; }
        public DbSet<ConversationMessage> ConversationMessages { get; set; }
        public DbSet<ConversationMessageContent> ConversationMessageContents { get; set; }
        public DbSet<ConversationMessageMediaContent> ConversationMessageMediaContents { get; set; }
        public DbSet<ConversationMessageViewer> ConversationMessageViewers { get; set; }
        public DbSet<UserLike> UsersLikes { get; set; }
        #endregion
    }
}
