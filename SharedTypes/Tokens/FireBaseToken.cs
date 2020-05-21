using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.Tokens
{
    public class FireBaseToken
    {
        [Key]
        public string Id { get; set; }
        public string Token { get; set; }
    }
}
