using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes.Tokens
{
    public class PayLoad
    {
        public string Token { get; set; }
        public string CallName { get; set; }
        public Contact Contact { get; set; }
    }
}
