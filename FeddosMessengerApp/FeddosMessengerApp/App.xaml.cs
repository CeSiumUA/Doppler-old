using FeddosMessengerApp.FireBase;
using FeddosMessengerApp.Hubs;
using FeddosMessengerApp.MobileDataBase;
using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeddosMessengerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            IFireBaseComponent fireBaseComponent = DependencyService.Get<IFireBaseComponent>();
            if (fireBaseComponent.CheckGooglePlayServicesAvailability())
            {
                //Personal personal = null;
                //using (MobileDataBaseContext mobileDataBase = new MobileDataBaseContext(DependencyService.Get<IGetPath>().GetDataBasePath("msngr.db")))
                //{
                //    personal = mobileDataBase.Personal.FirstOrDefault();
                //}
                string token = "";
                
                try
                {
                    token = Application.Current.Properties["JWT_Token"].ToString();
                }
                catch
                {

                }
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        //CommunicationHub.InitiateHub(personal.AuthServerToken);
                        CommunicationHub.InitiateHub(token);
                        MainPage = new MainPage();
                    }
                    catch(Exception ex)
                    {
                        Authentication();
                    }
                }
                else
                {
                    Authentication();
                }
                void Authentication()
                {
                    AuthPage authPage = new AuthPage();
                    MainPage = new NavigationPage(authPage);
                    authPage.OnAuthSuccessfullEvent += () =>
                    {
                        MainPage = new MainPage();
                    };
                }
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
