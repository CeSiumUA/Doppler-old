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

namespace FeddosMessengerApp
{
    //TODO
    class CommunicationHub
    {
        public static HubConnection hubConnection;
        public static void InitiateHub(string Token)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sllPolicyError) => true;
            //TODO
            hubConnection = new HubConnectionBuilder().WithUrl(Properties.Resources.ServerIPAddress + "/chat",
                options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(Token);
                }).Build();
            
        }

        public static void InitiateHub()
        {
            string Token = "";
            using (MobileDataBaseContext mdbc = new MobileDataBaseContext(DependencyService.Get<IGetPath>().GetDataBasePath("msngr.db")))
            {
                Token = mdbc.Personal.FirstOrDefault().AuthServerToken;
            }

            if (Token != "")
            {
                InitiateHub(Token);
            }
            else
            {
                throw new Exception("Token was null");
            }
        }
    }
}
