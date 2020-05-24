using Doppler.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doppler
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsPage : ContentPage
    {
        ContactsViewModel ContactsViewModel;
        public ContactsPage()
        {
            InitializeComponent();
            ContactsViewModel = new ContactsViewModel()
            {
                Navigation = this.Navigation
            };
            this.BindingContext = ContactsViewModel;
        }

        private void EntryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContactsViewModel.ReceiveContacts.Execute(e.NewTextValue);
        }
    }
}