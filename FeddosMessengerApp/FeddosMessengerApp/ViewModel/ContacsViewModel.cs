using FeddosMessengerApp.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FeddosMessengerApp.ViewModel
{
    public class ContacsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Contact> InnerContacts { get; set; }
        public List<Contact> ReceivedContacts { get; set; }
        public ICommand ReceiveContacts { get; set; }
        public ContacsViewModel()
        {
            CommunicationHub.hubConnection.On<List<Contact>>("ReceiveContacts", (contacts) =>
            {
                ReceivedContacts = contacts;
                OnPropertyChanged("ReceivedContacts");
            });
        }

        protected void OnPropertyChanged(string Name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }
        public void GetContacts(object pattern)
        {
            CommunicationHub.GetContacts(pattern);
        }
        
    }
}
