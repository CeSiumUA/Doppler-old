using System;
using System.Collections.Generic;
using System.Text;
using Doppler.API.Social;
using Doppler.API.Storage.FileStorage;

namespace Doppler.API.Storage.UserStorage
{
    public class ProfileImage : Image
    {
        public bool IsActive { get; set; }
        public User User { get; set; }
    }
}
