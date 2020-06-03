using DopplerREST.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DopplerREST.Database
{
    public class APIdataBaseContext:DbContext
    {
        //TODO
        public DbSet<User> Users { get; set; }
        public APIdataBaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        
    }
}
