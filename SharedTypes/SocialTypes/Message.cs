using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public class Message : IMessage
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public bool Dialog { get; set; }
        public string ChatId { get; set; }
        public string MessageText { get; set; }
    }
}
