using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FeddosMessengerApp.MobileDataBase;
using FeddosMessengerApp.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xamarin.Forms;
using System.Net.Http;
using SharedTypes.SocialTypes;

namespace FeddosMessengerApp.Hubs
{
    //TODO
    public class CommunicationHub
    {
        public static HubConnection hubConnection;
        public static async Task InitiateHub(string Token)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sllPolicyError) => true;
            //TODO
            hubConnection = new HubConnectionBuilder().WithUrl(Properties.Resources.ServerIPAddress + "/chat",
                options =>
                {
                    options.WebSocketConfiguration = conf =>
                    {
                        conf.RemoteCertificateValidationCallback = (message, cert, chain, errors) => { return true; };
                    };
                    options.HttpMessageHandlerFactory = factory => new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
                    };
                    options.AccessTokenProvider = () => Task.FromResult(Token);
                }).Build();
            
            await hubConnection.StartAsync();

        }
        public static async Task GetContacts(string pattern)
        {
            await hubConnection.InvokeAsync("GetContacts", pattern);
        }
        public static async Task<bool> CheckConnection()
        {
            hubConnection.On<bool>("CheckAuthResult", (s) =>
            {

            });
            bool bl = await hubConnection.InvokeAsync<bool>("CheckAuthorization");
            
            return bl;
        }
        public static async Task<List<Contact>> GetNewChats(string pattern)
        {
            List<Contact> gotContacts = await hubConnection.InvokeAsync<List<Contact>>("GetNewChats", pattern);
            return gotContacts;
        }
        //public static async void InitiateHub()
        //{
        //    string Token = "";
        //    using (MobileDataBaseContext mdbc = new MobileDataBaseContext(DependencyService.Get<IGetPath>().GetDataBasePath("msngr.db")))
        //    {
        //        Token = mdbc.Personal.FirstOrDefault().AuthServerToken;
        //    }

        //    if (Token != "")
        //    {
        //        await InitiateHub(Token);
        //    }
        //    else
        //    {
        //        throw new Exception("Token was null");
        //    }
        //}
    }
}
