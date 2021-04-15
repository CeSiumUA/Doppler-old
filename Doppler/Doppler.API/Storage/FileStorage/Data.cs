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
        public string? ContentType { get; set; }
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
            set
            {
                this.fileSize = value;
            }
        }

        private long? fileSize { get; set; }
    }
    public enum FileUploadType
    {
        FileAttachment = 0,
        ProfileImage = 1
    }
}
