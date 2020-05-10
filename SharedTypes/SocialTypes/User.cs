using SharedTypes.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string CallName { get; set; }
        public string Password { get; set; }
        public FireBaseToken FireBaseToken { get; set; }
        public Contact Contact { get; set; }
    }
}
