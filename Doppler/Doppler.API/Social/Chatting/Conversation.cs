using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Storage.UserStorage;

namespace Doppler.API.Social.Chatting
{
    public abstract class Conversation
    {
        [Key]
        public Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        [JsonIgnore]
        public ProfileImage Icon { get; set; }
        public List<ConversationMember> Members { get; set; }
        [NotMapped]
        public virtual string IconUrl
        {
            get
            {
                return string.IsNullOrEmpty(iconUrl) ? this.Icon.Id.ToString() : iconUrl;
            }
            set
            {
                this.iconUrl = value;
            }
        }
        private string iconUrl { get; set; }
    }
}
