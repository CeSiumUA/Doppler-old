using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doppler.API.Social.Chatting
{
    public class ConversationMessageViewer
    {
        [Key]
        public long Id { get; set; }
        public ConversationMember Member { get; set; }
        public  ConversationMessage Message { get; set; }
        public DateTime ViewedOn { get; set; }
        [DefaultValue(true)]
        public bool Viewed { get; set; }
        public ConversationMessageViewer()
        {
            this.ViewedOn = DateTime.UtcNow;
        }
    }
}
