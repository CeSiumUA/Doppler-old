using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public class Contact:IContact
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string CallName { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Icon { get; set; }
    }
}
