using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DopplerREST.Models
{
    public class ConnectionBuilder
    {
        [Key]
        public Guid Id { get; set; }
        public string FireBaseToken { get; set; }
        public Platform Platform { get; set; }
    }
    public enum Platform
    {
        Android = 0,
        iOS = 1,
        //actually, never gonna be used:
        Web = 2,
        UWP = 3
    }
}
