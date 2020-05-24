using SharedTypes.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.SocialTypes
{
    public interface IContact
    {
        [Key]
        Guid Id { get; set; }
        string Name { get; set; }
        string CallName { get; set; }
        string Description { get; set; }
        string PhoneNumber { get; set; }
        EntityIcon Icon { get; set; }
    }
}
