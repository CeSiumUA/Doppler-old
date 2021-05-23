using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.API.Storage;
using Doppler.REST.Models.Cryptography;
using Doppler.REST.Models.Social;
using Microsoft.EntityFrameworkCore;

namespace Doppler.REST.Models.Repository
{
    public interface IBackEndDbContextEntities : IDBContextEntities
    {
        public DbSet<Password> Passwords { get; set; }
        public DbSet<DopplerUser> DopplerUsers { get; set; }
    }
}
