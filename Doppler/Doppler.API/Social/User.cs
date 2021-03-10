using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Doppler.API.Authentication;

namespace Doppler.API.Social
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public List<UserContact> UserContacts { get; set; }
        public SignedInUser GetSignedInUser(JwtToken accessJwtToken, JwtToken refreshToken = null)
        {
            return new SignedInUser()
            {
                User = this,
                AccessToken = accessJwtToken,
                RefreshToken = refreshToken
            };
        }
    }
}
