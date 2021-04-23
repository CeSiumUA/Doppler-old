using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doppler.API.Social.Chatting
{
    public class ConversationMessage
    {
        [Key]
        public long Id { get; set; }
        public long? SenderId { get; set; }
        public ConversationMember Sender { get; set; }
        public long? ReceiverId { get; set; }
        public ConversationMember Receiver { get; set; }
        public bool Deleted { get; set; }
        public DateTime SentOn { get; set; }
        public long ContentId { get; set; }
        public ConversationMessageContent Content { get; set; }

        public ConversationMessage()
        {
            this.SentOn = DateTime.UtcNow;
        }
    }
}
