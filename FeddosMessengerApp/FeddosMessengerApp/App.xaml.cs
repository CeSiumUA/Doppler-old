using Doppler.DependencyInjections;
using Doppler.MobileDataBase;
using SharedTypes.Tokens;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doppler
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
                DataBaseClient.MobileDataBaseContext = new MobileDataBaseContext(DependencyService.Get<IGetPath>().GetDataBasePath("msngr.db"));
                string token = "";
                DateTime dateTime;
                try
                {
                    token = Application.Current.Properties["JWT_Token"].ToString();
                    dateTime = DateTime.Parse(Application.Current.Properties["Token_Time_Added"].ToString());
                    if (!string.IsNullOrEmpty(token))
                    {
                        double elapsedMilliSeconds = DateTime.Now.Subtract(dateTime).TotalMilliseconds;
                        if (elapsedMilliSeconds > (AuthenticationToken.TTL * 60000))
                        {
                            Authentication();
                        }
                        else
                        {
                            MainPage mainPage = new MainPage();
                            mainPage.InitiateHubAsync(token);
                            MainPage = mainPage;
                        }
                    }
                    else
                    {
                        Authentication();
                    }
                }
                catch
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
