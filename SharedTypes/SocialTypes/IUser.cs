using SharedTypes.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public interface IUser
    {
        [Key]
        Guid Id { get; set; }
        string CallName { get; set; }
        string Password { get; set; }
        FireBaseToken FireBaseToken { get; set; }
        Contact Contact { get; set; }
    }
}
