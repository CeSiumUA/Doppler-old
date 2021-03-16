using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Doppler.API.Authentication;
using Doppler.API.Storage.FileStorage;

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
        [JsonIgnore]
        public List<UserContact> UserContacts { get; set; }
        [JsonIgnore]
        public Data Icon { get; set; }
        [NotMapped]
        public string IconUrl
        {
            get
            {
                return Icon?.Id.ToString();
            }
        }
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
