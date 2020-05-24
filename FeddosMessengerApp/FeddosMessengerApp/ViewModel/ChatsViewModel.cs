using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Doppler.ViewModel
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
