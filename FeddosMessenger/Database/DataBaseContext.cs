using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using SharedTypes.SocialTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace FeddosMessenger.Database
{
    public class DataBaseContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> dbContextOptions):base(dbContextOptions)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            //dbContextOptionsBuilder.UseSqlServer(@"Server=DESKTOP-GNILB6P;Database=FeddosMessenger;Trusted_Connection=True");
        }
    }
}
