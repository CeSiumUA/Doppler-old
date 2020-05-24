using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public class MessageContent
    {
        [Key]
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
