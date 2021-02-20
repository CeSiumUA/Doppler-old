using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Social;

namespace Doppler.API.Storage
{
    public interface IDBContextEntities
    {
        public DbSet<User> Users { get; set; }
    }
}
