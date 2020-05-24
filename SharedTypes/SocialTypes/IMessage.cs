using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public interface IMessage
    {
        [Key]
        Guid Id { get; set; }
        MessageContent MessageContent { get; set; }
        string Sender { get; set; }
        bool Dialog { get; set; }
        string ChatId { get; set; }
    }
}
