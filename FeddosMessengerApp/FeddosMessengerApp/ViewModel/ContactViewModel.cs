using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FeddosMessengerApp.ViewModel
{
    public class ContactViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        ContactsViewModel contactsViewModel;

        public Contact Contact { get; private set; }

        public ContactViewModel()
        {
            Contact = new Contact();
        }
        public ContactsViewModel ListViewModel
        {
            get { return contactsViewModel; }
            set
            {
                if(contactsViewModel != value)
                {
                    contactsViewModel = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }
        public string Id
        {
            get { return Contact.Id; }
            set
            {
                if(Contact.Id != value)
                {
                    Contact.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Name { get { return Contact.Name; }
            set
            {
                if(Contact.Name != value)
                {
                    Contact.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string CallName
        {
            get
            {
                return Contact.CallName;
            }
            set
            {
                if(Contact.CallName != value)
                {
                    Contact.CallName = value;
                    OnPropertyChanged("CallName");
                }
            }
        }
        public string Description
        {
            get
            {
                return Contact.Description;
            }
            set
            {
                if (Contact.Description != value)
                {
                    Contact.Description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(Name.Trim())) || (!string.IsNullOrEmpty(CallName.Trim())) || (!string.IsNullOrEmpty(Description.Trim())));
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
