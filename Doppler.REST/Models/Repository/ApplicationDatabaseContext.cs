using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Social.Chatting;
using Doppler.API.Social.Likes;
using Doppler.API.Storage;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Storage.UserStorage;
using Doppler.REST.Models.Authentication;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Models.Repository;
using Doppler.REST.Models.Social;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Doppler.REST.Models
{
    public class ApplicationDatabaseContext : DbContext, IBackEndDbContextEntities
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
        private readonly IConfiguration configuration;
        public ApplicationDatabaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration["Doppler:DatabaseConnectionString"]);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(x => x.Login).IsUnique(true);
            modelBuilder.Entity<User>().HasMany(user => user.UserContacts).WithOne(user => user.User);
            modelBuilder.Entity<User>().HasMany(x => x.UserLikes).WithOne(x => x.LikedUser);
            modelBuilder.Entity<ConversationMessage>().HasOne(message => message.Content).WithOne(content => content.Message).HasForeignKey<ConversationMessageContent>(x => x.Id);
            //modelBuilder.Entity<Dialogue>().HasOne(x => x.FirstUser).WithMany();
            //modelBuilder.Entity<Dialogue>().HasOne(x => x.SecondUser).WithMany();
            base.OnModelCreating(modelBuilder);
        }
    }
}
