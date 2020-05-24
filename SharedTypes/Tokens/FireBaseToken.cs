using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.Tokens
{
    public class FireBaseToken
    {
        [Key]
        public Guid Id { get; set; }
        public string Token { get; set; }
        public PlatformType PlatformType { get; set; }
    }
    public enum PlatformType
    {
        Android = 0,
        iOS
    }
}
