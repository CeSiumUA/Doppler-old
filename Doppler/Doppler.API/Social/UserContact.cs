using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Doppler.API.Social
{
    public class UserContact
    {
        [Key]
        public Guid Id { get; set; }
        public User User { get; set; }
        public User Contact { get; set; }

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(displayName))
                {
                    return Contact.Name;
                }

                return displayName;
            }
            set
            {
                displayName = value;
            }
        }
        private string displayName { get; set; }
    }
}
