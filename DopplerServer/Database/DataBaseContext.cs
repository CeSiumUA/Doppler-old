
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SharedTypes.SocialTypes;

using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DopplerServer.Database
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(@"Server=DESKTOP-GNILB6P;Database=Doppler;Trusted_Connection=True");
        }
    }
}
