using FeddosMessengerApp.MobileDataBase;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FeddosMessengerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public delegate void OnAuthSuccessfullHandler();
        public event OnAuthSuccessfullHandler OnAuthSuccessfullEvent;
        private const string URL = "https://192.168.0.154:5001/auth";
        public AuthPage()
        {
            InitializeComponent();
        }

        private async void AuthBox_Clicked(object sender, EventArgs e)
        {
            string name = usernameBox.Text;
            string password = passwordBox.Text;
            string FireBaseToken = DependencyService.Get<IFireBaseComponent>().GetFireBaseToken();
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = Encoding.UTF8.GetBytes("username=" + name + "&password=" + password + "&firebasetoken=" + FireBaseToken);
            httpWebRequest.ContentLength = bytes.Length;
            using (Stream stream = httpWebRequest.GetRequestStream()) 
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                WebResponse webResponse = await httpWebRequest.GetResponseAsync();
                using(StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    (string Token, string CallName) authTuple = JsonConvert.DeserializeObject<(string Token, string CallName)>(streamReader.ReadToEnd());
                    IGetPath getPath = DependencyService.Get<IGetPath>();
                    using (MobileDataBaseContext mdbc = new MobileDataBaseContext(getPath.GetDataBasePath("msngr.db")))
                    {
                        Personal pers = null;
                        if (mdbc.Personal.Count() > 0)
                        {
                            pers = mdbc.Personal.FirstOrDefault();
                        }
                        else
                        {
                            pers = new Personal();

                            pers = new Personal()
                            {
                                PassWord = password,
                                AuthServerToken = authTuple.Token,
                                UserName = authTuple.CallName,
                                FireBaseToken = FireBaseToken
                            };
                            await mdbc.Personal.AddAsync(pers);
                        }
                        await mdbc.SaveChangesAsync();
                    }

                    OnAuthSuccessfullEvent.Invoke();
                }
            }
            catch(System.Net.WebException exc)
            {

            }
        }
    }
}