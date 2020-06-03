using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DopplerREST.Models
{
    public class User
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Contact Contact { get; set; }
        public ConnectionBuilder ConnectionBuilder { get; set; }
    }
}
