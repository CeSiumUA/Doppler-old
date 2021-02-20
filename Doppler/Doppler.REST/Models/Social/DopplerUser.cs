using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Doppler.API.Social;
using Doppler.REST.Models.Authentication;

namespace Doppler.REST.Models.Social
{
    public class DopplerUser : User
    {
        [JsonIgnore]
        public Password Password { get; set; }
    }
}
