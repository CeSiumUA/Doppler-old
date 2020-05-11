using SharedTypes.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public interface IUser
    {
        string CallName { get; set; }
        string Password { get; set; }
        FireBaseToken FireBaseToken { get; set; }
        Contact Contact { get; set; }
        List<Contact> Contacts { get; set; }
    }
}
