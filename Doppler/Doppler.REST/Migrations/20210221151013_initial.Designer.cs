﻿// <auto-generated />
using System;
using Doppler.REST.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Doppler.REST.Migrations
{
    [DbContext(typeof(ApplicationDatabaseContext))]
    [Migration("20210221151013_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Doppler.API.Social.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Doppler.REST.Models.Authentication.Password", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("Doppler.REST.Models.Social.DopplerUser", b =>
                {
                    b.HasBaseType("Doppler.API.Social.User");

                    b.Property<int?>("PasswordId")
                        .HasColumnType("int");

                    b.HasIndex("PasswordId");

                    b.HasDiscriminator().HasValue("DopplerUser");
                });

            modelBuilder.Entity("Doppler.REST.Models.Authentication.Password", b =>
                {
                    b.HasOne("Doppler.API.Social.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Doppler.REST.Models.Social.DopplerUser", b =>
                {
                    b.HasOne("Doppler.REST.Models.Authentication.Password", "Password")
                        .WithMany()
                        .HasForeignKey("PasswordId");

                    b.Navigation("Password");
                });
#pragma warning restore 612, 618
        }
    }
}