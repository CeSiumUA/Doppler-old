using Microsoft.EntityFrameworkCore;
using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelperApplication
{
    public class DBCNT:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DBCNT()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-GNILB6P;Database=Doppler;Trusted_Connection=True;");
        }
    }
}
