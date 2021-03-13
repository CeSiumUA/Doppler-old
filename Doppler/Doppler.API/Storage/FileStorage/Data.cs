using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doppler.API.Storage.FileStorage
{
    public class Data
    {
        [Key] 
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public BLOB BLOB { get; set; }

        public long? FileSize
        {
            get
            {
                if (BLOB == null)
                {
                    return fileSize;
                }

                return BLOB.Data.LongLength;
            }
        }

        private long? fileSize { get; set; }
    }
}
