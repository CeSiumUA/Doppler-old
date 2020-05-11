﻿using Microsoft.EntityFrameworkCore;
using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FeddosMessengerApp.MobileDataBase
{
    public class MobileDataBaseContext:DbContext
    {
        private string DbPath;
        public DbSet<MContact> Contacts { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public MobileDataBaseContext(string DbPath)
        {
            this.DbPath = DbPath;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlite($"Filename={DbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
