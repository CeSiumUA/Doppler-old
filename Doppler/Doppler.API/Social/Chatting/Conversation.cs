using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Doppler.API.Storage.FileStorage;

namespace Doppler.API.Social.Chatting
{
    public class Conversation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ConversationMember> Members { get; set; }
        public Data Icon { get; set; }
    }
}
