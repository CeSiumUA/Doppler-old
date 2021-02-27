using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doppler.API.Authentication
{
    public class JwtToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
