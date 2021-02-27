using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Doppler.API.Social;

namespace Doppler.REST.Models.Cryptography
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int Iterations { get; set; }

        public byte[] GetHashBytes()
        {
            return string.IsNullOrEmpty(this.PasswordHash) ? null : Convert.FromBase64String(this.PasswordHash);
        }
        public byte[] GetSaltBytes()
        {
            return string.IsNullOrEmpty(this.PasswordSalt) ? null : Convert.FromBase64String(this.PasswordSalt);
        }
    }
}
