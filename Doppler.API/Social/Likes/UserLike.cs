using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doppler.API.Social.Likes
{
    public class UserLike
    {
        [Key]
        public long Id { get; set; }
        public User Liker{ get; set; }
        public User LikedUser { get; set; }
        public bool IsLiked { get; set; }
    }
}
