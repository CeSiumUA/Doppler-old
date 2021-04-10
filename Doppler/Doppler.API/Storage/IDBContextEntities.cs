using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Social;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Storage.UserStorage;

namespace Doppler.API.Storage
{
    public interface IDBContextEntities
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Data> Files { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
    }
}
