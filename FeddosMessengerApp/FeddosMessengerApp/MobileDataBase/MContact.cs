using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FeddosMessengerApp.MobileDataBase
{
    public class MContact : IContact
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get ; set ; }
        public string CallName { get ; set ; }
        public string Description { get ; set ; }
        public string PhoneNumber { get ; set ; }
        public byte[] Icon { get ; set ; }
    }
}
