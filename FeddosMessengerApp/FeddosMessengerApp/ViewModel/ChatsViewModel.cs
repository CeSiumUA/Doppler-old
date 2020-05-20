using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FeddosMessengerApp.ViewModel
{
    public class ChatsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Chat[] Chats;

        public ChatsViewModel()
        {

        }
    }
}
