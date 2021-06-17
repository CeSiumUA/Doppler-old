using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Doppler.API.Social.Chatting
{
    public class ConversationMessageContent
    {
        [Key]
        public Guid Id { get; set; }
        //public Guid MessageId { get; set; }
        //[JsonIgnore]
        //public ConversationMessage Message { get; set; }
        public string Text { get; set; }
        public List<ConversationMessageMediaContent> MediaContents { get; set; }
    }
}
