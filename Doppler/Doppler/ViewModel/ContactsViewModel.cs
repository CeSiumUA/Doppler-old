using Doppler.Hubs;
using Doppler.MobileDataBase;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Doppler.ViewModel
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ContactViewModel> InnerContacts { get; set; }
        public ObservableCollection<ContactViewModel> ReceivedContacts { get; set; }
        public ICommand ReceiveContacts { get; protected set; }
        public ICommand SaveContactCommand { get; protected set; }
        public string SearchPattern { get; set; }
        public ContactViewModel selectedContact;
        public INavigation Navigation { get; set; }
        public ContactsViewModel()
        {
            Title = "Знайдено в БД";
            ReceiveContacts = new Command(GetContacts);
            SaveContactCommand = new Command(SaveContact);
            CommunicationHub.hubConnection.On<List<Contact>>("ReceiveContacts", (contacts) =>
            {
                ReceivedContacts = new ObservableCollection<ContactViewModel>();
                foreach(Contact cont in contacts)
                {
                    ContactViewModel contactViewModel = new ContactViewModel();
                    contactViewModel.Name = cont.Name;
                    contactViewModel.CallName = cont.CallName;
                    contactViewModel.Description = cont.Description;
                    contactViewModel.Id = cont.Id;
                    ReceivedContacts.Add(contactViewModel);
                }
                OnPropertyChanged("ReceivedContacts");
            });
        }
        public ContactViewModel SelectedContact
        {
            get { return selectedContact; }
            set
            {
                if(selectedContact != value)
                {
                    ContactViewModel selCont = value;
                    selCont.ListViewModel = this;
                    selectedContact = null;
                    OnPropertyChanged("SelectedContact");
                    Navigation.PushAsync(new ContactPage(selCont));
                }
            }
        }
        protected void OnPropertyChanged(string Name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
        }
        public async void GetContacts(object pattern)
        {
            await CommunicationHub.GetContacts(SearchPattern);
        }
        public async void SaveContact(object ContactModel)
        {
           
            Contact contact = ((ContactViewModel)ContactModel).Contact;

            try
            {
                await DataBaseClient.MobileDataBaseContext.Contacts.AddAsync(contact);
                await DataBaseClient.MobileDataBaseContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
                
            
            await Navigation.PopAsync();
        }
    }
}
