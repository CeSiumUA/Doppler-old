using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public class Chat : IChat
    {
        public string Id { get; set; }
        public string ChatName { get; set; }
        public string Members { get; set; }
    }
}
