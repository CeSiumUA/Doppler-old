using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Doppler.API.Authentication;
using Doppler.API.Social.Likes;
using Doppler.API.Storage.FileStorage;
using Doppler.API.Storage.UserStorage;
using Microsoft.EntityFrameworkCore;

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
        public string Description { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public List<UserContact> UserContacts { get; set; }
        [JsonIgnore]
        public List<ProfileImage> Icons { get; set; }
        [JsonIgnore]
        public List<UserLike> UserLikes { get; set; }
        [NotMapped]
        public string IconUrl
        {
            get
            {
                return Icons?.FirstOrDefault(x => x.IsActive)?.Id.ToString();
            }
        }
        [NotMapped]
        public List<UserIconURL> UserIcons
        {
            get
            {
                return Icons?.OrderByDescending(x => x.UploadDate).OrderByDescending(x => x.IsActive ? 1 : 0).Select(x =>
                {
                    return new UserIconURL()
                    {
                        URL = x.Id,
                        IsActive = x.IsActive
                    };
                }).ToList();
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

        public long? Likes
        {
            get
            {
                return this.UserLikes?.Where(x => x.LikedUser.Id == this.Id && x.IsLiked).LongCount();
            }
        }
    }

    public class UserIconURL
    {
        public Guid URL { get; set; }
        public bool IsActive { get; set; }
    }
}
