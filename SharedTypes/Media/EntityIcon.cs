using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTypes.Media
{
    public class EntityIcon
    {
        [Key]
        public Guid Id { get; set; }
        public byte[] Icon { get; set; }
    }
}
