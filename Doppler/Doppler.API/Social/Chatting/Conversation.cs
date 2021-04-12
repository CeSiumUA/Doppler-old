using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Storage.UserStorage;

namespace Doppler.API.Social.Chatting
{
    public class Conversation
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDialogue { get; set; }
        public List<ConversationMember> Members { get; set; }
        public ProfileImage Icon { get; set; }
    }
}
