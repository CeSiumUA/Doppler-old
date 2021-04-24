using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Doppler.API.Storage.FileStorage;

namespace Doppler.API.Social.Chatting
{
    public class ConversationMessageMediaContent
    {
        [Key]
        public long Id { get; set; }
        public Guid ConversationMessageContentId { get; set; }
        [JsonIgnore]
        public ConversationMessageContent ConversationMessageContent { get; set; }
        [DefaultValue(MediaContentType.File)]
        public MediaContentType ContentType { get; set; }
        public Guid DataId { get; set; }
        [JsonIgnore]
        public Data Data { get; set; }
    }

    public enum MediaContentType
    {
        Photo,
        Viedo,
        Audio,
        File,
        Other
    }
}
