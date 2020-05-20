using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public interface IChat
    {
        string Id { get; set; }
        string ChatName { get; set; }
        string Members { get; set; }
    }
}
