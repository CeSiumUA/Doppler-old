using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DopplerREST.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
