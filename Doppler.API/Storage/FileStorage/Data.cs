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
        public DateTime UploadDate { get; set; }
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

        public Data()
        {
            this.UploadDate = DateTime.UtcNow;
        }
        public FileAccessLevel AccessLevel { get; set; }
        private long? fileSize { get; set; }
    }
    public enum FileUploadType
    {
        FileAttachment = 0,
        ProfileImage = 1
    }

    public enum FileAccessLevel
    {
        Public = 0,
        Users = 1,
        Conversation = 2,
        Private = 3,
        System =4
    }
}
