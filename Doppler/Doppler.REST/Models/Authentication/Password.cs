using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Doppler.API.Social;

namespace Doppler.REST.Models.Authentication
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public User User { get; set; }
    }
}
