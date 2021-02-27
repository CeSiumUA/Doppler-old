using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Social;
using Doppler.API.Storage;
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
    }
}
