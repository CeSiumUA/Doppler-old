using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FeddosMessengerApp.MobileDataBase
{
    public class Personal
    {
        [Key]
        public Guid Id { get; set; }
        public string FireBaseToken { get; set; }
        public string AuthServerToken { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public MContact Contact { get; set; }
    }
}
