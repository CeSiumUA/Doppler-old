using FeddosMessengerApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeddosMessengerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        ContactViewModel contactViewModel { get; set; }
        public ContactPage(ContactViewModel contactViewModel)
        {
            InitializeComponent();
            this.contactViewModel = contactViewModel;
            this.BindingContext = this.contactViewModel;
        }
    }
}