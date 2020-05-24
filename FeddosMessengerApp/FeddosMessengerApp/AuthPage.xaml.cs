using Doppler.DependencyInjections;
using Doppler.Hubs;
using Doppler.MobileDataBase;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedTypes.SocialTypes;
using SharedTypes.Tokens;
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

namespace Doppler
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthPage : ContentPage
    {
        public delegate void OnAuthSuccessfullHandler();
        public event OnAuthSuccessfullHandler OnAuthSuccessfullEvent;
        private readonly string URL;
        public AuthPage()
        {
            InitializeComponent();
            URL = Properties.Resources.ServerIPAddress;
        }

        private async void AuthBox_Clicked(object sender, EventArgs e)
        {
            IGetPath getPath = DependencyService.Get<IGetPath>();
            string name = usernameBox.Text;
            string password = passwordBox.Text;
            string FireBaseToken = DependencyService.Get<IFireBaseComponent>().GetFireBaseToken();
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL + "/auth");
            httpWebRequest.ServerCertificateValidationCallback = delegate { return true; };
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = Encoding.UTF8.GetBytes("username=" + name + "&password=" + password + "&firebasetoken=" + FireBaseToken + "&platform=" + DependencyService.Get<IGetPlatform>().GetPlatform().ToString());
            httpWebRequest.ContentLength = bytes.Length;
            using (Stream stream = httpWebRequest.GetRequestStream()) 
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                WebResponse webResponse = await httpWebRequest.GetResponseAsync();
                string Token = "";
                using(StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    string json = streamReader.ReadToEnd();
                    PayLoad authTuple = JsonConvert.DeserializeObject<PayLoad>(json);
                    //using (MobileDataBaseContext mdbc = new MobileDataBaseContext(getPath.GetDataBasePath("msngr.db")))
                    //{
                    //    Personal pers = null;
                    //    if (mdbc.Personal.Count() > 0)
                    //    {
                    //        pers = mdbc.Personal.FirstOrDefault();
                    //    }
                    //    Token = authTuple.Token;
                    //    pers = new Personal()
                    //    {
                    //        //PassWord = password,
                    //        //AuthServerToken = authTuple.Token,
                    //        UserName = authTuple.CallName,
                    //        //FireBaseToken = FireBaseToken,
                    //        Contact = new Contact()
                    //        {
                    //            Id = authTuple.Contact.Id,
                    //            Name = authTuple.Contact.Name,
                    //            CallName = authTuple.Contact.CallName,
                    //            Description = authTuple.Contact.Description,
                    //            Icon = authTuple.Contact.Icon,
                    //            PhoneNumber = authTuple.Contact.PhoneNumber
                    //        }
                    //    };
                    Token = authTuple.Token;

                    Application.Current.Properties.Remove("JWT_Token");
                    Application.Current.Properties.Add("JWT_Token", Token);

                    Application.Current.Properties.Remove("Token_Time_Added");
                    Application.Current.Properties.Add("Token_Time_Added", DateTime.Now.ToString());

                    Application.Current.Properties.Remove("Personal");
                    Application.Current.Properties.Add("Personal", JsonConvert.SerializeObject(new Personal()
                    {
                        UserName = authTuple.CallName,
                        //PassWord = password,
                        Contact = authTuple.Contact
                    }));
                    
                    await Application.Current.SavePropertiesAsync();
                    //    await mdbc.Personal.AddAsync(pers);

                    //    await mdbc.SaveChangesAsync();
                    //}
                   
                }
                await CommunicationHub.InitiateHub(Token);
                
                OnAuthSuccessfullEvent.Invoke();

            }
            catch(System.Net.WebException exc)
            {
                AuthResult.Text = "Не вдалося аутентифікувати користувача";
            }
        }
    }
}