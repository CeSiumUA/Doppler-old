﻿// <auto-generated />
using System;
using Doppler.REST.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doppler.REST.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    partial class ApplicationDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Doppler.API.Authentication.JwtToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Token");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.Conversation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("IconId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IconId");

                    b.ToTable("Conversations");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Conversation");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMember", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("ConversationMembers");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientGeneratedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<long?>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SentOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("ConversationMessages");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMessageContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConversationMessageContents");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMessageMediaContent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContentType")
                        .HasColumnType("int");

                    b.Property<Guid>("ConversationMessageContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DataId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ConversationMessageContentId");

                    b.HasIndex("DataId");

                    b.ToTable("ConversationMessageMediaContents");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMessageViewer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("MemberId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Viewed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ViewedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("MessageId");

                    b.ToTable("ConversationMessageViewers");
                });

            modelBuilder.Entity("Doppler.API.Social.Likes.UserLike", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.Property<int?>("LikedUserId")
                        .HasColumnType("int");

                    b.Property<int?>("LikerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LikedUserId");

                    b.HasIndex("LikerId");

                    b.ToTable("UsersLikes");
                });

            modelBuilder.Entity("Doppler.API.Social.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasFilter("[Login] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Doppler.API.Social.UserContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersContacts");
                });

            modelBuilder.Entity("Doppler.API.Storage.FileStorage.BLOB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Blobs");
                });

            modelBuilder.Entity("Doppler.API.Storage.FileStorage.Data", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("BLOBId")
                        .HasColumnType("int");

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BLOBId");

                    b.ToTable("Files");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Data");
                });

            modelBuilder.Entity("Doppler.REST.Models.Cryptography.Password", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Iterations")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.Dialogue", b =>
                {
                    b.HasBaseType("Doppler.API.Social.Chatting.Conversation");

                    b.Property<long?>("FirstUserId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SecondUserId")
                        .HasColumnType("bigint");

                    b.HasIndex("FirstUserId");

                    b.HasIndex("SecondUserId");

                    b.HasDiscriminator().HasValue("Dialogue");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.Group", b =>
                {
                    b.HasBaseType("Doppler.API.Social.Chatting.Conversation");

                    b.HasDiscriminator().HasValue("Group");
                });

            modelBuilder.Entity("Doppler.REST.Models.Social.DopplerUser", b =>
                {
                    b.HasBaseType("Doppler.API.Social.User");

                    b.Property<int?>("PasswordId")
                        .HasColumnType("int");

                    b.Property<string>("RefreshTokenToken")
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("PasswordId");

                    b.HasIndex("RefreshTokenToken");

                    b.HasDiscriminator().HasValue("DopplerUser");
                });

            modelBuilder.Entity("Doppler.API.Storage.UserStorage.Image", b =>
                {
                    b.HasBaseType("Doppler.API.Storage.FileStorage.Data");

                    b.HasDiscriminator().HasValue("Image");
                });

            modelBuilder.Entity("Doppler.API.Storage.UserStorage.ProfileImage", b =>
                {
                    b.HasBaseType("Doppler.API.Storage.UserStorage.Image");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("ProfileImage");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.Conversation", b =>
                {
                    b.HasOne("Doppler.API.Storage.UserStorage.ProfileImage", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");

                    b.Navigation("Icon");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMember", b =>
                {
                    b.HasOne("Doppler.API.Social.Chatting.Conversation", "Conversation")
                        .WithMany("Members")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doppler.API.Social.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMessage", b =>
                {
                    b.HasOne("Doppler.API.Social.Chatting.ConversationMessageContent", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doppler.API.Social.Chatting.ConversationMember", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId");

                    b.HasOne("Doppler.API.Social.Chatting.ConversationMember", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId");

                    b.Navigation("Content");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMessageMediaContent", b =>
                {
                    b.HasOne("Doppler.API.Social.Chatting.ConversationMessageContent", "ConversationMessageContent")
                        .WithMany("MediaContents")
                        .HasForeignKey("ConversationMessageContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doppler.API.Storage.FileStorage.Data", "Data")
                        .WithMany()
                        .HasForeignKey("DataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConversationMessageContent");

                    b.Navigation("Data");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMessageViewer", b =>
                {
                    b.HasOne("Doppler.API.Social.Chatting.ConversationMember", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Doppler.API.Social.Chatting.ConversationMessage", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Doppler.API.Social.Likes.UserLike", b =>
                {
                    b.HasOne("Doppler.API.Social.User", "LikedUser")
                        .WithMany("UserLikes")
                        .HasForeignKey("LikedUserId");

                    b.HasOne("Doppler.API.Social.User", "Liker")
                        .WithMany()
                        .HasForeignKey("LikerId");

                    b.Navigation("LikedUser");

                    b.Navigation("Liker");
                });

            modelBuilder.Entity("Doppler.API.Social.UserContact", b =>
                {
                    b.HasOne("Doppler.API.Social.User", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId");

                    b.HasOne("Doppler.API.Social.User", "User")
                        .WithMany("UserContacts")
                        .HasForeignKey("UserId");

                    b.Navigation("Contact");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doppler.API.Storage.FileStorage.Data", b =>
                {
                    b.HasOne("Doppler.API.Storage.FileStorage.BLOB", "BLOB")
                        .WithMany()
                        .HasForeignKey("BLOBId");

                    b.Navigation("BLOB");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.Dialogue", b =>
                {
                    b.HasOne("Doppler.API.Social.Chatting.ConversationMember", "FirstUser")
                        .WithMany()
                        .HasForeignKey("FirstUserId");

                    b.HasOne("Doppler.API.Social.Chatting.ConversationMember", "SecondUser")
                        .WithMany()
                        .HasForeignKey("SecondUserId");

                    b.Navigation("FirstUser");

                    b.Navigation("SecondUser");
                });

            modelBuilder.Entity("Doppler.REST.Models.Social.DopplerUser", b =>
                {
                    b.HasOne("Doppler.REST.Models.Cryptography.Password", "Password")
                        .WithMany()
                        .HasForeignKey("PasswordId");

                    b.HasOne("Doppler.API.Authentication.JwtToken", "RefreshToken")
                        .WithMany()
                        .HasForeignKey("RefreshTokenToken");

                    b.Navigation("Password");

                    b.Navigation("RefreshToken");
                });

            modelBuilder.Entity("Doppler.API.Storage.UserStorage.ProfileImage", b =>
                {
                    b.HasOne("Doppler.API.Social.User", "User")
                        .WithMany("Icons")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.Conversation", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("Doppler.API.Social.Chatting.ConversationMessageContent", b =>
                {
                    b.Navigation("MediaContents");
                });

            modelBuilder.Entity("Doppler.API.Social.User", b =>
                {
                    b.Navigation("Icons");

                    b.Navigation("UserContacts");

                    b.Navigation("UserLikes");
                });
#pragma warning restore 612, 618
        }
    }
}
