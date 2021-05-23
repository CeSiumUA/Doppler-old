using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doppler.API.Storage.FileStorage
{
    public class BLOB
    {
        [Key]
        public int Id { get; set; }
        public byte[] Data { get; set; }
    }
}
