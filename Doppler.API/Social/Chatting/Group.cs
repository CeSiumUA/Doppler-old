using System;
using System.Collections.Generic;
using System.Text;

namespace Doppler.API.Social.Chatting
{
    public class Group : Conversation
    {
        public List<ConversationMember> Members { get; set; }
    }
}
