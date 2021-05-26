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
        public long Id { get; set; }
        public Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        [MaxLength(7)]
        public string Color { get; set; }
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
        public int UserId { get; set; } 
        public User User { get; set; }
        private string displayName { get; set; }

        public ConversationMember()
        {
            var rnd = new Random();
            string color = "#FFFFFF";
            do
            {
                color = String.Format("#{0:x6}", rnd.Next(0x1000000));
            } while (color == "#000000" || color == "#FFFFFF");

            this.Color = color;
        }
    }

    public enum ConversationMemberRole
    {
        Reader,
        Member,
        Admin,
        Owner
    }
}
