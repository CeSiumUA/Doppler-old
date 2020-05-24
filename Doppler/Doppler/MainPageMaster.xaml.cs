using Doppler.MobileDataBase;
using SharedTypes.SocialTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doppler
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            Contact cntc = Newtonsoft.Json.JsonConvert.DeserializeObject<Personal>(Application.Current.Properties["Personal"].ToString()).Contact;
            InitializeComponent();
            HeadetLabel.Text = cntc.Name;
            using (Stream ms = new MemoryStream(cntc.Icon.Icon))
            {
                profileImage.Source = ImageSource.FromStream(() => { return ms; });
            }
            BindingContext = new MainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }

            public MainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
                {
                    new MainPageMasterMenuItem { Id = 0, Title = "Чати", TargetType=typeof(ChatsPage) },
                    new MainPageMasterMenuItem { Id = 1, Title = "Контакти", TargetType=typeof(ContactsPage) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}