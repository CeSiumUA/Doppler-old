using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Storage.UserStorage;

namespace Doppler.API.Social.Chatting
{
    public abstract class Conversation
    {
        [Key]
        public Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public ProfileImage Icon { get; set; }
    }
}
