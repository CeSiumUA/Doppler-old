using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doppler.API.Social.Chatting
{
    public class ConversationMember
    {
        [Key]
        public int Id { get; set; }
        public Conversation Conversation { get; set; }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(displayName))
                {
                    return User?.Name;
                }

                return displayName;
            }
            set
            {
                displayName = value;
            }
        }
        [DefaultValue(ConversationMemberRole.Member)]
        public ConversationMemberRole Role { get; set; }
        public User User { get; set; }
        private string displayName { get; set; }
    }

    public enum ConversationMemberRole
    {
        Reader,
        Member,
        Admin,
        Owner
    }
}
