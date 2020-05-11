using SharedTypes.SocialTypes;
using SharedTypes.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FeddosMessengerApp.MobileDataBase
{
    public class MUser : IUser
    {
        [Key]
        public Guid Id { get; set; }
        public string CallName { get; set ; }
        public string Password { get ; set ; }
        public FireBaseToken FireBaseToken { get ; set ; }
        public Contact Contact { get ; set ; }
        public List<Contact> Contacts { get ; set ; }
    }
}
