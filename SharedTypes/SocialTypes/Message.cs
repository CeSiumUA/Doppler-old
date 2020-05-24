using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public class Message : IMessage
    {
        [Key]
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public bool Dialog { get; set; }
        public string ChatId { get; set; }
        public MessageContent MessageContent { get; set; }
    }
}
