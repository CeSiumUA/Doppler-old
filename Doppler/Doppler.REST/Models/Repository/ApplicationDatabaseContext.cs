using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Social.Chatting;
using Doppler.API.Storage;
using Doppler.API.Storage.FileStorage;
using Doppler.REST.Models.Authentication;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Models.Social;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Doppler.REST.Models
{
    public class ApplicationDatabaseContext : DbContext, IDBContextEntities
    {
        #region DBSet
        public DbSet<Password> Passwords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DopplerUser> DopplerUsers { get; set; }
        public DbSet<JwtToken> RefreshTokens { get; set; }
        public DbSet<UserContact> UsersContacts { get; set; }
        #endregion

        #region FileStorage
        public DbSet<BLOB> Blobs { get; set; }
        public DbSet<Data> Files { get; set; }
        #endregion
        #region SocialDbSet
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationMember> ConversationMembers { get; set; }
        public DbSet<ConversationMessage> ConversationMessages { get; set; }
        public DbSet<ConversationMessageContent> ConversationMessageContents { get; set; }
        public DbSet<ConversationMessageMediaContent> ConversationMessageMediaContents { get; set; }
        public DbSet<ConversationMessageViewer> ConversationMessageViewers { get; set; }
        #endregion
        private readonly IConfiguration configuration;
        public ApplicationDatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration["Doppler:DatabaseConnectionString"]);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Login).IsUnique(true);
            modelBuilder.Entity<User>().HasMany(user => user.UserContacts).WithOne(user => user.User);
            modelBuilder.Entity<ConversationMessage>().HasOne(message => message.Content).WithOne(content => content.Message).HasForeignKey<ConversationMessageContent>(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
