using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Doppler.Models
{
    public class MChat : IChat
    {
        public string Id { get; set; }
        public string ChatName { get; set; }
        public string Members { get; set; }
        public Message LastMessage { get; set; }
    }
}
