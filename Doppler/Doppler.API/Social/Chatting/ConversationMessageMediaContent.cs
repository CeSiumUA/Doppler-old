using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Doppler.API.Storage.FileStorage;

namespace Doppler.API.Social.Chatting
{
    public class ConversationMessageMediaContent
    {
        [Key]
        public long Id { get; set; }
        public ConversationMessageContent ConversationMessageContent { get; set; }
        [DefaultValue(MediaContentType.File)]
        public MediaContentType ContentType { get; set; }
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
