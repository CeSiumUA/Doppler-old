﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Doppler.REST.Models.Social;

namespace Doppler.REST.Models.Repository
{
    public interface IRepository
    {
        public Task<DopplerUser> GetDopplerUserWithPassword(string login);
    }
}
