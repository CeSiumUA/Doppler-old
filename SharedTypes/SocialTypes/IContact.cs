using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public interface IContact
    {
        string FirstName { get; set; }
        string SecondName { get; set; }
        string CallName { get; set; }
        string Description { get; set; }
        string PhoneNumber { get; set; }
        byte[] Icon { get; set; }
    }
}
