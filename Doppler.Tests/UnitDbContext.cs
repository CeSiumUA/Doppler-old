using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Doppler.API.Authentication;
using Doppler.API.Social;
using Doppler.API.Social.Chatting;
using Doppler.API.Social.Likes;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Storage.UserStorage;
using Doppler.REST.Models;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Models.Repository;
using Doppler.REST.Models.Social;
using Microsoft.EntityFrameworkCore;

namespace Doppler.Tests
{
    public class UnitDbContext : ApplicationDatabaseContext, IBackEndDbContextEntities
    {

        public UnitDbContext() : base(null)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=DopplerDB;Trusted_Connection=True;");
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
